using System;
using System.Collections.Generic;

namespace AHSW.EventLog.Contracts.Interfaces.Configurators;

public interface IEntityLogConfigurator<TEventType, TEntityType, in TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    public IEntityLogConfigurator<TEventType, TEntityType, TPropertyType> AddEntityLogging<TEntity>(
        IEnumerable<TEntity> entities, Func<TPropertyType[]> getObservableProperties)
            where TEntity : class;
}