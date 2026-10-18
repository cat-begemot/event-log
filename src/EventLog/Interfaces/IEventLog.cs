using System;
using AHSW.EventLog.Contracts.Interfaces;
using AHSW.EventLog.Models.Configuration;

namespace AHSW.EventLog.Interfaces;

public interface IEventLog<TEventType, TEntityType, TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    EventLogConfiguration<TEventType, TEntityType, TPropertyType> Configuration { get; }
    
    IApplicationRepository ApplicationRepository { get; }
}