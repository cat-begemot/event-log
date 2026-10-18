using System;
using System.Collections.Generic;
using AHSW.EventLog.Contracts.Interfaces;
using AHSW.EventLog.Contracts.Models;
using AHSW.EventLog.Interfaces;
using AHSW.EventLog.Interfaces.Configuration;

namespace AHSW.EventLog.Models.Configuration;

internal class PropertyConfiguration<TEntity, TPropertyType> :
    IPropertyConfigurator<TEntity, TPropertyType>
        where TEntity : class
        where TPropertyType : struct, Enum
{
    private readonly Dictionary<TPropertyType, IPropertyInfo> _propertyInfos = new();

    public IReadOnlyDictionary<TPropertyType, IPropertyInfo> PropertyInfos => _propertyInfos;
    
    public IPropertyConfigurator<TEntity, TPropertyType> RegisterProperty(TPropertyType propertyType,
        Func<TEntity, object> propertyGetter, string propertyName)
    {
        _propertyInfos[propertyType] = new PropertyInfo<TEntity>(propertyGetter, propertyName);
        return this;
    }
}