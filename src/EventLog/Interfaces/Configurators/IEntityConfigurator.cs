using System;

namespace AHSW.EventLog.Interfaces.Configurators;

public interface IEntityConfigurator<in TEntityType, in TPropertyType>
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    IEntityConfigurator<TEntityType, TPropertyType> RegisterEntity<TEntity>(
        TEntityType entityType, Func<object, int> getId,
        Action<IPropertyConfigurator<TEntity, TPropertyType>> optionsBuilder)
            where TEntity : class;
}