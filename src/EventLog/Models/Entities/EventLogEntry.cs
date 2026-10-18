using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AHSW.EventLog.Models.Entities.Abstract;
using AHSW.EventLog.Models.Enums;

namespace AHSW.EventLog.Models.Entities;

public class EventLogEntry<TEventType, TEntityType, TPropertyType> : ReadOnlyEntity
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    public EventLogEntry()
    {
    }
    
    public EventLogEntry(TEventType eventType)
    {
        EventType = eventType;
        CreatedAt = DateTime.UtcNow;
    }
    
    public TEventType EventType { get; set; }
    
    public ICollection<EntityLogEntry<TEventType, TEntityType, TPropertyType>> EntityLog { get; set; }
    
    public DateTime CompletedAt { get; set; }
    
    public string Details { get; set; }
    
    public EventStatus Status { get; set; }
    
    public string FailureDetails { get; set; }
    
    /// <summary>
    /// Contains True if an exception thrown explicitly after logging the event in order to return an appropriate response to a client
    /// In this case IEventLogService does not save the additional unhandled exception into the model
    /// </summary>
    [NotMapped]
    public bool ExplicitlyThrownException { get; set; }

    [NotMapped]
    public ICollection<FailureInfo> FailureInfos { get; set; }
}