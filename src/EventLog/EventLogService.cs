using System.Text.Json;
using System.Text.Json.Serialization;
using AHSW.EventLog.Extensions;
using AHSW.EventLog.Interfaces;
using AHSW.EventLog.Interfaces.Configurators;
using AHSW.EventLog.Models;
using AHSW.EventLog.Models.Configurations;
using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Models.Enums;

namespace AHSW.EventLog;

public class EventLogService<TEventType, TEntityType, TPropertyType> :
    IEventLogService<TEventType, TEntityType, TPropertyType>,
    IEventLog<TEventType, TEntityType, TPropertyType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum

{
    private static EventLogConfiguration<TEventType, TEntityType, TPropertyType> _logConfiguration;
    private static readonly JsonSerializerOptions _serializerOptions;
    
    private readonly IApplicationRepository _applicationRepository;

    static EventLogService()
    {
        _serializerOptions = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }
    
    public EventLogService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }
    
    public static async Task ConfigureAsync(Func<IEventLogConfigurator<TEventType, TEntityType, TPropertyType>, Task> configurationBuilder = null)
    {
        ArgumentNullException.ThrowIfNull(configurationBuilder);
        _logConfiguration = new EventLogConfiguration<TEventType, TEntityType, TPropertyType>();
        await configurationBuilder.Invoke(_logConfiguration);
    }

    EventLogConfiguration<TEventType, TEntityType, TPropertyType> IEventLog<TEventType, TEntityType, TPropertyType>.Configuration => _logConfiguration;
    
    IApplicationRepository IEventLog<TEventType, TEntityType, TPropertyType>.ApplicationRepository => _applicationRepository;
    
    public async Task CreateEventScopeAndRun(TEventType eventLogType,
        Func<EventLogScope<TEventType, TEntityType, TPropertyType>, Task> workUnitAction)
    {
        var eventLogEntry = new EventLogEntry<TEventType, TEntityType, TPropertyType>(eventLogType);;
        
        try
        {
            await workUnitAction(
                new EventLogScope<TEventType, TEntityType, TPropertyType>(this, eventLogEntry));
            
            if (eventLogEntry.Status != EventStatus.HandledException)
                eventLogEntry.Status = EventStatus.Successful;
            
            await AddOrUpdateEventLogAsync(eventLogEntry);
        }
        catch (TaskCanceledException exception)
        {
            await ProcessUnhandledException(EventStatus.TaskCancelledException, eventLogEntry, exception);
            throw;
        }
        catch (Exception exception)
        {
            await ProcessUnhandledException(EventStatus.UnhandledException, eventLogEntry, exception);
            throw;
        }
    }
    
    public async Task<TResult> CreateEventScopeAndRun<TResult>(TEventType eventLogType,
        Func<EventLogScope<TEventType, TEntityType, TPropertyType>, Task<TResult>> workUnitAction)
    {
        var eventLogEntry = new EventLogEntry<TEventType, TEntityType, TPropertyType>(eventLogType);;
        
        try
        {
            var result = await workUnitAction(
                new EventLogScope<TEventType, TEntityType, TPropertyType>(this, eventLogEntry));
            
            if (eventLogEntry.Status != EventStatus.HandledException)
                eventLogEntry.Status = EventStatus.Successful;
            
            await AddOrUpdateEventLogAsync(eventLogEntry);

            return result;
        }
        catch (TaskCanceledException exception)
        {
            await ProcessUnhandledException(EventStatus.TaskCancelledException, eventLogEntry, exception);
            throw;
        }
        catch (Exception exception)
        {
            await ProcessUnhandledException(EventStatus.UnhandledException, eventLogEntry, exception);
            throw;
        }
    }
    
    private async Task ProcessUnhandledException(EventStatus eventStatus,
        EventLogEntry<TEventType, TEntityType, TPropertyType> eventLogEntry,
        Exception exception)
    {
        if (!eventLogEntry.ExplicitlyThrownException)
            eventLogEntry.AddFailureInfo(eventStatus, exception: exception);
        
        await AddOrUpdateEventLogAsync(eventLogEntry);
    }

    private async Task AddOrUpdateEventLogAsync(
        EventLogEntry<TEventType, TEntityType, TPropertyType> eventLogEntry)
    {
        if (eventLogEntry.FailureInfos != null && eventLogEntry.FailureInfos.Any())
        {
            eventLogEntry.FailureDetails = JsonSerializer.Serialize(
                eventLogEntry.FailureInfos, _serializerOptions);
        }
        
        await _applicationRepository.AddOrUpdateEventLogAsync(eventLogEntry);
    }
}