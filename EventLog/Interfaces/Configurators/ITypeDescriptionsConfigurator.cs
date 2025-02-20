using AHWS.EventLog.Models.Enums;

namespace AHWS.EventLog.Interfaces.Configurators;

public interface ITypeDescriptionsConfigurator<in TEventType>
    where TEventType : struct, Enum
{
    ITypeDescriptionsConfigurator<TEventType> AddEventTypeDescription(
        TEventType eventType, string description);
    
    ITypeDescriptionsConfigurator<TEventType> AddEventStatusDescription(
        EventStatus eventType, string description);
}