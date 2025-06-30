using AHSW.EventLog.Interfaces;
using AHSW.EventLog.Interfaces.Configurators;
using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Models.Entities.Abstract;
using AHSW.EventLog.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace AHSW.EventLog.Models.Configurations;

public class EventLogConfiguration<TEventType, TEntityType, TPropertyType> :
    IEventLogConfigurator<TEventType, TEntityType, TPropertyType>,
    IEntityConfigurator<TEntityType, TPropertyType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    private readonly Dictionary<TEventType, string> _eventTypeDescriptions = new();
    private readonly Dictionary<TEntityType, string> _entityTypeDescriptions = new();
    private readonly Dictionary<TPropertyType, string> _propertyTypeDescriptions = new();
    private readonly Dictionary<EventStatus, string> _eventStatusDescriptions = new();

    private readonly Dictionary<Type, Func<object, int>> _entityIdGetters = new();
    private readonly Dictionary<Type, TEntityType> _entityTypes = new();
    private readonly Dictionary<TPropertyType, IPropertyInfo> _propertyInfos = new();
    
    public async Task<IEntityConfigurator<TEntityType, TPropertyType>> UseCustomTypeDescriptionsAsync(DbContext context,
        Action<ITypeDescriptionsConfigurator<TEventType, TEntityType, TPropertyType>> optionsBuilder)
    {
        var configurator = CreateDefaultTypeDescriptionsConfiguration();
        optionsBuilder(configurator);
        
        foreach (var pair in configurator.EventTypeDescriptions)
            _eventTypeDescriptions[pair.Key] = pair.Value;
        
        foreach (var pair in configurator.EntityTypeDescriptions)
            _entityTypeDescriptions[pair.Key] = pair.Value;
        
        foreach (var pair in configurator.PropertyTypeDescriptions)
            _propertyTypeDescriptions[pair.Key] = pair.Value;
        
        foreach (var pair in configurator.EventStatusDescriptions)
            _eventStatusDescriptions[pair.Key] = pair.Value;
        
        await FillCustomDescriptionTables(context);
        
        return this;
    }

    public IEntityConfigurator<TEntityType, TPropertyType> RegisterEntity<TEntity>(
        TEntityType entityType, Func<object, int> getId, Action<IPropertyConfigurator<TEntity, TPropertyType>> optionsBuilder)
            where TEntity : class
    {
        _entityTypes[typeof(EntityLogInfo<TEntity, TPropertyType>)] = entityType;
        _entityIdGetters[typeof(TEntity)] = getId;

        var configurator = new PropertyConfiguration<TEntity, TPropertyType>();
        optionsBuilder(configurator);

        foreach (var property in configurator.PropertyInfos)
            _propertyInfos[property.Key] = property.Value;
        
        return this;
    }
    
    
    public TEntityType GetEntityType<TEntity>(EntityLogInfo<TEntity, TPropertyType> logInfo)
        where TEntity : class
    {
        if (_entityTypes.TryGetValue(logInfo.GetType(), out var entityType))
            return entityType;
        
        throw new NotImplementedException($"The type {nameof(EntityLogInfo<TEntity, TPropertyType>)} cannot be parsed into {nameof(TEntityType)}");
    }
    
    public int GetEntityId<TEntity>(TEntity entity)
        where TEntity : class
    {
        if (_entityIdGetters.TryGetValue(entity.GetType(), out var entityIdGetter))
            return entityIdGetter(entity);
        
        throw new NotImplementedException($"The ID getter for the entity type {nameof(TEntity)} is not registered");
    }
    
    public PropertyValues GetPropertyInfo<TEntity>(TEntity entity,
        TPropertyType propertyType, Func<TEntity, string, object> getOriginalPropertyValue)
        where TEntity : class
    {
        if (_propertyInfos.TryGetValue(propertyType, out var propertyInfo))
        {
            // think about improve here
            if (propertyInfo is PropertyInfo<TEntity> targetPropertyInfo)
            {
                return new PropertyValues(
                    getOriginalPropertyValue(entity, targetPropertyInfo.Name),
                    targetPropertyInfo.Getter(entity));
            }
        }
                
        throw new Exception($"Not found a registered property for the {nameof(TPropertyType)}.{propertyType}");
    }
    
    private async Task FillCustomDescriptionTables(DbContext databaseContext)
    {
        await UpdateStorage<TEventType, EventTypeDescription>(_eventTypeDescriptions);
        await UpdateStorage<TEntityType, EntityTypeDescription>(_entityTypeDescriptions);
        await UpdateStorage<TPropertyType, PropertyTypeDescription>(_propertyTypeDescriptions);
        await UpdateStorage<EventStatus, EventStatusDescription>(_eventStatusDescriptions);
        
        await databaseContext.SaveChangesAsync();

        return;
        
        async Task UpdateStorage<TEnum, TDescriptiveEntity>(IReadOnlyDictionary<TEnum, string> descriptions)
            where TDescriptiveEntity : BaseDescriptiveEntity, new()
            where TEnum : struct, Enum
        {
            var descriptionEntities = GetCustomEnumDescriptions<TEnum, TDescriptiveEntity>(descriptions);
            await databaseContext.Set<TDescriptiveEntity>().ExecuteDeleteAsync();
            await databaseContext.Set<TDescriptiveEntity>().AddRangeAsync(descriptionEntities);
        }
    }
    
    private static IReadOnlyCollection<TDescriptiveEntity> GetCustomEnumDescriptions<TEnum, TDescriptiveEntity>(
        IReadOnlyDictionary<TEnum, string> enumDescriptions)
            where TDescriptiveEntity : BaseDescriptiveEntity, new()
            where TEnum : struct, Enum
    {
        var enumValues = Enum.GetValues<TEnum>();
        var entities = new List<TDescriptiveEntity>(enumValues.Length);
        
        entities.AddRange(enumValues.Select(value =>
            new TDescriptiveEntity()
            {
                EnumId = Convert.ToInt32(value),
                Description = enumDescriptions?.TryGetValue(value, out var description) ?? false
                    ? description
                    : Enum.GetName(value)
            }));

        return entities;
    }
    
    private TypeDescriptionsConfiguration<TEventType, TEntityType, TPropertyType> CreateDefaultTypeDescriptionsConfiguration()
    {
        var configurator = new TypeDescriptionsConfiguration<TEventType, TEntityType, TPropertyType>();
        
        configurator
            .AddEventStatusDescription(EventStatus.Successful, "Successful")
            .AddEventStatusDescription(EventStatus.HandledException, "Handled exception")
            .AddEventStatusDescription(EventStatus.UnhandledException, "Unhandled exception");

        return configurator;
    }
}