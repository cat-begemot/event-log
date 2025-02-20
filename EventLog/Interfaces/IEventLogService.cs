using AHWS.EventLog.Models;

namespace AHWS.EventLog.Interfaces;

public interface IEventLogService<TEventType, TEntityType, TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    /// <summary>
    /// Create internally EventLogEntry object, wraps the work unit, executes it and
    /// logs a result to the EventLogEntry object
    /// </summary>
    Task CreateEventScopeAndRun(TEventType eventLogType,
        Func<EventLogScope<TEventType, TEntityType, TPropertyType>, Task> workUnitAction);
    
    /// <summary>
    /// Create internally EventLogEntry object, wraps the work unit, executes it and
    /// logs a result to the EventLogEntry object
    /// </summary>
    Task<TResult> CreateEventScopeAndRun<TResult>(TEventType eventLogType,
        Func<EventLogScope<TEventType, TEntityType, TPropertyType>, Task<TResult>> workUnitAction);
}