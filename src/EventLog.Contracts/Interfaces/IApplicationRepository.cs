using System;
using System.Threading;
using System.Threading.Tasks;
using AHSW.EventLog.Contracts.Models.Entities;

namespace AHSW.EventLog.Contracts.Interfaces;

public interface IApplicationRepository
{
    Task AddOrUpdateEventLogAsync<TEventType, TEntityType, TPropertyType>(
        EventLogEntry<TEventType, TEntityType, TPropertyType> entity,
        CancellationToken cancellationToken = default)
            where TEventType : struct, Enum
            where TEntityType : struct, Enum
            where TPropertyType : struct, Enum;

    object GetOriginalPropertyValue<TEntity>(TEntity entity, string propertyName)
        where TEntity : class;
}