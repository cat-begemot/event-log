using System;

namespace AHSW.EventLog.Interfaces.Configuration;

public interface IPropertyConfigurator<out TEntity, in TPropertyType>
    where TEntity : class
    where TPropertyType : struct, Enum
{
    IPropertyConfigurator<TEntity, TPropertyType> RegisterProperty(TPropertyType propertyType,
        Func<TEntity, object> propertyGetter, string propertyName);
}