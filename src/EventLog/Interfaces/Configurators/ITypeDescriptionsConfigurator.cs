using System;
using AHSW.EventLog.Models.Enums;

namespace AHSW.EventLog.Interfaces.Configurators;

public interface ITypeDescriptionsConfigurator<in TEventType, in TEntityType, in TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    ITypeDescriptionsConfigurator<TEventType, TEntityType, TPropertyType> AddEventTypeDescription(
        TEventType eventType, string description);

    ITypeDescriptionsConfigurator<TEventType, TEntityType, TPropertyType> AddEntityTypeDescription(
        TEntityType entityType, string description);

    ITypeDescriptionsConfigurator<TEventType, TEntityType, TPropertyType> AddPropertyTypeDescription(
        TPropertyType propertyType, string description);
    
    ITypeDescriptionsConfigurator<TEventType, TEntityType, TPropertyType> AddEventStatusDescription(
        EventStatus eventType, string description);
}