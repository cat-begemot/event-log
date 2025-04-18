﻿namespace AHSW.EventLog.Interfaces.Configurators;

public interface IEntityLogConfigurator<TEventType, TEntityType, in TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    IEntityLogConfigurator<TEventType, TEntityType, TPropertyType> AddEntityLogging<TEntity>(
        IEnumerable<TEntity> entities, Func<TPropertyType[]> getObservableProperties)
            where TEntity : class;
}