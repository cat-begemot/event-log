using System;
using System.Collections.Generic;
using AHSW.EventLog.Contracts.Models;

namespace AHSW.EventLog.Contracts.Interfaces.Configurators;

public interface IEntityLogConfiguration<TEventType, TEntityType, in TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    IEntityLogConfigurator<TEventType, TEntityType, TPropertyType> AddEntityLogging<TEntity>(
        IEnumerable<TEntity> entities, Func<TPropertyType[]> getObservableProperties)
            where TEntity : class;
}