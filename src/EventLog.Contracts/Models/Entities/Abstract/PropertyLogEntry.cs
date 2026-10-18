using System;

namespace AHSW.EventLog.Contracts.Models.Entities.Abstract;

public abstract class PropertyLogEntry<T, TEventType, TEntityType, TPropertyType> :
    PkEntity
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    public TPropertyType PropertyType { get; set; }
    
    public T Value { get; set; }
    
    public int EntityLogEntryId { get; set; }

    public EntityLogEntry<TEventType, TEntityType, TPropertyType> EntityLogEntry { get; set; }
}