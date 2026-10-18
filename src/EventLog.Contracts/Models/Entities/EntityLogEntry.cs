using System;
using System.Collections.Generic;
using System.Linq;
using AHSW.EventLog.Contracts.Models.Entities.Abstract;
using AHSW.EventLog.Contracts.Models.Entities.PropertyLogEntries;
using AHSW.EventLog.Contracts.Models.Enums;

namespace AHSW.EventLog.Contracts.Models.Entities;

public class EntityLogEntry<TEventType, TEntityType, TPropertyType> : PkEntity
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    public ActionType ActionType { get; set; }
    
    public TEntityType EntityType { get; set; }

    public int EntityId { get; set; }
    
    public int EventLogEntryId { get; set; }
    
    public EventLogEntry<TEventType, TEntityType, TPropertyType> EventLogEntry { get; set; }
    
    public ICollection<BoolPropertyLogEntry<TEventType, TEntityType, TPropertyType>> BoolPropertyLog { get; set; }
    
    public ICollection<DateTimePropertyLogEntry<TEventType, TEntityType, TPropertyType>> DateTimePropertyLog { get; set; }
    
    public ICollection<StringPropertyLogEntry<TEventType, TEntityType, TPropertyType>> StringPropertyLog { get; set; }
    
    public ICollection<Int32PropertyLogEntry<TEventType, TEntityType, TPropertyType>> Int32PropertyLog { get; set; }
    
    public ICollection<DoublePropertyLogEntry<TEventType, TEntityType, TPropertyType>> DoublePropertyLog { get; set; }
    
    public ICollection<DecimalPropertyLogEntry<TEventType, TEntityType, TPropertyType>> DecimalPropertyLog { get; set; }

    public bool HasPropertyLogEntries =>
        (BoolPropertyLog != null && BoolPropertyLog.Any()) ||
        (DateTimePropertyLog != null && DateTimePropertyLog.Any()) ||
        (StringPropertyLog != null && StringPropertyLog.Any()) ||
        (Int32PropertyLog != null && Int32PropertyLog.Any()) ||
        (DoublePropertyLog != null && DoublePropertyLog.Any()) ||
        (DecimalPropertyLog != null && DecimalPropertyLog.Any());
}