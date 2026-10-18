using System;
using AHSW.EventLog.Models.Configurations;

namespace AHSW.EventLog.Interfaces;

public interface IEventLog<TEventType, TEntityType, TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    EventLogConfiguration<TEventType, TEntityType, TPropertyType> Configuration { get; }
    
    IApplicationRepository ApplicationRepository { get; }
}