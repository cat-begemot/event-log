using System;
using System.Collections.Generic;
using AHSW.EventLog.Interfaces.Configurators;
using AHSW.EventLog.Models.Enums;

namespace AHSW.EventLog.Models.Configurations;

public class TypeDescriptionsConfiguration<TEventType, TEntityType, TPropertyType> :
    ITypeDescriptionsConfigurator<TEventType, TEntityType, TPropertyType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    private readonly Dictionary<TEventType, string> _eventTypeDescriptions = new();
    private readonly Dictionary<TEntityType, string> _entityTypeDescriptions = new();
    private readonly Dictionary<TPropertyType, string> _propertyTypeDescriptions = new();
    private readonly Dictionary<EventStatus, string> _eventStatusDescriptions = new();
    
    public IReadOnlyDictionary<TEventType, string> EventTypeDescriptions => _eventTypeDescriptions;
    
    public IReadOnlyDictionary<TEntityType, string> EntityTypeDescriptions => _entityTypeDescriptions;
    
    public IReadOnlyDictionary<TPropertyType, string> PropertyTypeDescriptions => _propertyTypeDescriptions;
    
    public IReadOnlyDictionary<EventStatus, string> EventStatusDescriptions => _eventStatusDescriptions;
    
    public ITypeDescriptionsConfigurator<TEventType, TEntityType, TPropertyType> AddEventTypeDescription(
        TEventType eventType, string description)
    {
        _eventTypeDescriptions[eventType] = description;
        return this;
    }
    
    public ITypeDescriptionsConfigurator<TEventType, TEntityType, TPropertyType> AddEntityTypeDescription(
        TEntityType entityType, string description)
    {
        _entityTypeDescriptions[entityType] = description;
        return this;
    }
    
    public ITypeDescriptionsConfigurator<TEventType, TEntityType, TPropertyType> AddPropertyTypeDescription(
        TPropertyType propertyType, string description)
    {
        _propertyTypeDescriptions[propertyType] = description;
        return this;
    }
    
    public ITypeDescriptionsConfigurator<TEventType, TEntityType, TPropertyType> AddEventStatusDescription(
        EventStatus eventStatus, string description)
    {
        _eventStatusDescriptions[eventStatus] = description;
        return this;
    }
}