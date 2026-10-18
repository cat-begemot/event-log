using System;
using System.Threading.Tasks;
using AHSW.EventLog.Contracts.Interfaces.Configurators;
using Microsoft.EntityFrameworkCore;

namespace AHSW.EventLog.Interfaces.Configuration;

public interface IEventLogConfigurator<in TEventType, TEntityType, TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    public Task<IEntityConfigurator<TEntityType, TPropertyType>> UseCustomTypeDescriptionsAsync(DbContext context,
        Action<ITypeDescriptionsConfigurator<TEventType, TEntityType, TPropertyType>> optionsBuilder);

    public IEntityConfigurator<TEntityType, TPropertyType> RegisterEntity<TEntity>(
        TEntityType entityType, Func<object, int> getId,
        Action<IPropertyConfigurator<TEntity, TPropertyType>> optionsBuilder)
        where TEntity : class;
}