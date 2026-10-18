using System;
using AHSW.EventLog.Models.Entities.Abstract;

namespace AHSW.EventLog.Models.Entities.PropertyLogEntries;

public class Int32PropertyLogEntry<TEventType, TEntityType, TPropertyType> :
    PropertyLogEntry<int?, TEventType, TEntityType, TPropertyType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
}