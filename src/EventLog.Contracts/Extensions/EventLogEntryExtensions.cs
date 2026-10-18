using System;
using System.Collections.Generic;
using AHSW.EventLog.Contracts.Models;
using AHSW.EventLog.Contracts.Models.Entities;
using AHSW.EventLog.Contracts.Models.Enums;

namespace AHSW.EventLog.Contracts.Extensions;

public static class EventLogEntryExtensions
{
    public static void AddFailureInfo<TEventType, TEntityType, TPropertyType>(
        this EventLogEntry<TEventType, TEntityType, TPropertyType> model,
        EventStatus failedStatus, string message = null, Exception exception = null,
        bool explicitlyThrownException = false)
            where TEventType : struct, Enum
            where TEntityType : struct, Enum
            where TPropertyType : struct, Enum
    {
        model.Status = failedStatus;
        model.ExplicitlyThrownException = explicitlyThrownException;
        
        model.FailureInfos ??= new List<FailureInfo>();
        model.FailureInfos.Add(new FailureInfo()
        {
            Status = Enum.GetName(failedStatus),
            Message = message,
            ExceptionMessage = exception?.Message,
            StackTrace = exception?.StackTrace,
        });
    }
}