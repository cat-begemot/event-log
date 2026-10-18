using System;
using AHSW.EventLog.Contracts.Models.Entities.Abstract;

namespace AHSW.EventLog.Contracts.Models.Entities.PropertyLogEntries;

public class DateTimePropertyLogEntry<TEventType, TEntityType, TPropertyType> :
    PropertyLogEntry<DateTime?, TEventType, TEntityType, TPropertyType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
}