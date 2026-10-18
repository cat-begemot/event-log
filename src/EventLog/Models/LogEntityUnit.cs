using System;
using AHSW.EventLog.Models.Entities;

namespace AHSW.EventLog.Models;

public class LogEntityUnit<TEventType, TEntityType, TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    private readonly object _entity;
    private readonly Func<object, int> _getEntityId;
    private readonly EntityLogEntry<TEventType, TEntityType, TPropertyType> _entityLogEntry;

    public LogEntityUnit(object entity, Func<object, int> getEntityId,
        EntityLogEntry<TEventType, TEntityType, TPropertyType> entityLogEntry)
    {
        _entity = entity;
        _getEntityId = getEntityId;
        _entityLogEntry = entityLogEntry;
    }

    public EntityLogEntry<TEventType, TEntityType, TPropertyType> GetEntityLogEntry()
    {
        _entityLogEntry.EntityId = _getEntityId(_entity);
        return _entityLogEntry;
    }
}