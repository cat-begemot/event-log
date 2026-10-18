using System;
using AHSW.EventLog.Contracts.Models.Entities.Abstract;

namespace AHSW.EventLog.Contracts.Models.Entities.PropertyLogEntries;

public class DoublePropertyLogEntry<TEventType, TEntityType, TPropertyType> :
    PropertyLogEntry<double?, TEventType, TEntityType, TPropertyType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
}