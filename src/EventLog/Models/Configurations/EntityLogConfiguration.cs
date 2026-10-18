using System;
using System.Collections.Generic;
using System.Linq;
using AHSW.EventLog.Interfaces;
using AHSW.EventLog.Interfaces.Configurators;
using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Models.Entities.Abstract;
using AHSW.EventLog.Models.Entities.PropertyLogEntries;
using AHSW.EventLog.Models.Enums;

namespace AHSW.EventLog.Models.Configurations;

public class EntityLogConfiguration<TEventType, TEntityType, TPropertyType> :
    IEntityLogConfigurator<TEventType, TEntityType, TPropertyType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    private readonly IEventLog<TEventType, TEntityType, TPropertyType> _eventLog;

    public EntityLogConfiguration(IEventLog<TEventType, TEntityType, TPropertyType> eventLog)
    {
        _eventLog = eventLog;
    }
    
    private readonly List<LogEntityUnit<TEventType, TEntityType, TPropertyType>> _logEntityUnits = new();

    public IReadOnlyCollection<LogEntityUnit<TEventType, TEntityType, TPropertyType>> LogEntityUnits => _logEntityUnits;
    
    public IEntityLogConfigurator<TEventType, TEntityType, TPropertyType> AddEntityLogging<TEntity>(
        IEnumerable<TEntity> entities, Func<TPropertyType[]> getObservableProperties)
            where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(entities);
        ArgumentNullException.ThrowIfNull(getObservableProperties);

        var logEntityUnits = GetLogEntities(new EntityLogInfo<TEntity, TPropertyType>(entities, getObservableProperties()));
        
        _logEntityUnits.AddRange(logEntityUnits);
        
        return this;
    }
    
    private IEnumerable<LogEntityUnit<TEventType, TEntityType, TPropertyType>> GetLogEntities<TEntity>(
        EntityLogInfo<TEntity, TPropertyType> logInfo)
            where TEntity : class
    {
        var entityType = _eventLog.Configuration.GetEntityType(logInfo);

        return logInfo.Entities
            .Select(entity =>
            {
                var entityLogEntry = new EntityLogEntry<TEventType, TEntityType, TPropertyType>()
                {
                    ActionType = IsNew()
                        ? ActionType.Create
                        : ActionType.Update,
                    EntityType = entityType
                };

                if (logInfo.Properties != null)
                {
                    foreach (var property in logInfo.Properties)
                        AddPropertyLogEntries(entity, property, entityLogEntry);
                }

                return new LogEntityUnit<TEventType, TEntityType, TPropertyType>(entity, _eventLog.Configuration.GetEntityId, entityLogEntry);
                
                bool IsNew()
                {
                    var id = _eventLog.Configuration.GetEntityId(entity);
                    return id == 0;
                }
            })
            .ToList();
    }
    
    private void AddPropertyLogEntries<TEntity>(TEntity entity, TPropertyType property,
        EntityLogEntry<TEventType, TEntityType, TPropertyType> entityLogEntry)
            where TEntity : class
    {
        var propertyValues = _eventLog.Configuration.GetPropertyInfo(
            entity, property, _eventLog.ApplicationRepository.GetOriginalPropertyValue);

        var indicativeProperty = propertyValues.New ?? propertyValues.Original;
        
        if (indicativeProperty == null)
            return;

        if (indicativeProperty is Enum)
            indicativeProperty = NullableCast<int>(indicativeProperty);
        
        switch (indicativeProperty)
        {
            case bool:
                TryCreateAndAddPropertyLogEntry<BoolPropertyLogEntry<TEventType, TEntityType, TPropertyType>, bool?>(
                    property, NullableCast<bool>(propertyValues.Original), NullableCast<bool>(propertyValues.New), entityLogEntry,
                    entityLogEntry.BoolPropertyLog != null,
                    x => entityLogEntry.BoolPropertyLog = x,
                    x => entityLogEntry.BoolPropertyLog.Add(x));
                
                break;
            
            case DateTime:
                TryCreateAndAddPropertyLogEntry<DateTimePropertyLogEntry<TEventType, TEntityType, TPropertyType>, DateTime?>(
                    property, NullableCast<DateTime>(propertyValues.Original), NullableCast<DateTime>(propertyValues.New), entityLogEntry,
                    entityLogEntry.DateTimePropertyLog != null,
                    x => entityLogEntry.DateTimePropertyLog = x,
                    x => entityLogEntry.DateTimePropertyLog.Add(x));
                
                break;
            
            case string:
                TryCreateAndAddPropertyLogEntry<StringPropertyLogEntry<TEventType, TEntityType, TPropertyType>, string>(
                    property, (string)propertyValues.Original, (string)propertyValues.New, entityLogEntry,
                    entityLogEntry.StringPropertyLog != null,
                    x => entityLogEntry.StringPropertyLog = x,
                    x => entityLogEntry.StringPropertyLog.Add(x));
                
                break;
            
            case int:
                TryCreateAndAddPropertyLogEntry<Int32PropertyLogEntry<TEventType, TEntityType, TPropertyType>, int?>(
                    property, NullableCast<int>(propertyValues.Original), NullableCast<int>(propertyValues.New), entityLogEntry,
                    entityLogEntry.Int32PropertyLog != null,
                    x => entityLogEntry.Int32PropertyLog = x,
                    x => entityLogEntry.Int32PropertyLog.Add(x));
                
                break;
            
            case double:
                TryCreateAndAddPropertyLogEntry<DoublePropertyLogEntry<TEventType, TEntityType, TPropertyType>, double?>(
                    property, NullableCast<double>(propertyValues.Original), NullableCast<double>(propertyValues.New), entityLogEntry,
                    entityLogEntry.DoublePropertyLog != null,
                    x => entityLogEntry.DoublePropertyLog = x,
                    x => entityLogEntry.DoublePropertyLog.Add(x));
                
                break;
            
            case decimal:
                TryCreateAndAddPropertyLogEntry<DecimalPropertyLogEntry<TEventType, TEntityType, TPropertyType>, decimal?>(
                    property, (decimal?)propertyValues.Original, (decimal?)propertyValues.New, entityLogEntry,
                    entityLogEntry.DecimalPropertyLog != null,
                    x => entityLogEntry.DecimalPropertyLog = x,
                    x => entityLogEntry.DecimalPropertyLog.Add(x));
                
                break;
            
            default:
                throw new NotImplementedException(nameof(property));
        }
    }
    
    private static void TryCreateAndAddPropertyLogEntry<TPropertyLogEntry, TLogValue>(
        TPropertyType propertyType, TLogValue originalValue, TLogValue newValue,
        EntityLogEntry<TEventType, TEntityType, TPropertyType> entityLogEntry, bool isLogInitialized,
        Action<ICollection<TPropertyLogEntry>> collectionSetter,Action<TPropertyLogEntry> addItem)
            where TPropertyLogEntry : PropertyLogEntry<TLogValue, TEventType, TEntityType, TPropertyType>, new()
    {
        if (!IsNewEntity() && newValue != null && newValue.Equals(originalValue))
            return;
        
        var propertyLogEntry = new TPropertyLogEntry
        {
            PropertyType = propertyType,
            Value = newValue,
            EntityLogEntry = entityLogEntry
        };
        
        if (!isLogInitialized)
            collectionSetter(new List<TPropertyLogEntry>());

        addItem(propertyLogEntry);

        return;

        bool IsNewEntity() => entityLogEntry.ActionType == ActionType.Create;
    }
    
    private static TValue? NullableCast<TValue>(object value)
        where TValue : struct =>
            value != null ? (TValue)value : null;
}