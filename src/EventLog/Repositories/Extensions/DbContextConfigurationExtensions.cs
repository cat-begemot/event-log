using System;
using AHSW.EventLog.Repositories.Configuration;
using Microsoft.EntityFrameworkCore;

namespace AHSW.EventLog.Repositories.Extensions;

public static class DbContextConfigurationExtensions
{
    public static ModelBuilder ApplyEventLogConfigurations<TEventType, TEntityType, TPropertyType>(this ModelBuilder modelBuilder)
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum =>
            modelBuilder
                .ApplyConfiguration(new EventLogEntryConfiguration<TEventType, TEntityType, TPropertyType>())
                .ApplyConfiguration(new EntityLogEntryConfiguration<TEventType, TEntityType, TPropertyType>())
                .ApplyConfiguration(new BoolPropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType>())
                .ApplyConfiguration(new DateTimePropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType>())
                .ApplyConfiguration(new StringPropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType>())
                .ApplyConfiguration(new Int32PropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType>())
                .ApplyConfiguration(new DoublePropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType>())
                .ApplyConfiguration(new DecimalPropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType>())
                .ApplyConfiguration(new EventTypeDescriptionConfiguration<TEventType>())
                .ApplyConfiguration(new EventStatusDescriptionConfiguration())
                .ApplyConfiguration(new EntityTypeDescriptionConfiguration())
                .ApplyConfiguration(new PropertyTypeDescriptionConfiguration<TPropertyType>());
}