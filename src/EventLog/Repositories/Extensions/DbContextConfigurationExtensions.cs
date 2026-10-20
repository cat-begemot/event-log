using System;
using AHSW.EventLog.Repositories.Configuration;
using Microsoft.EntityFrameworkCore;

namespace AHSW.EventLog.Repositories.Extensions;

public static class DbContextConfigurationExtensions
{
    public static ModelBuilder ApplyEventLogConfigurations<TEventType, TEntityType, TPropertyType>(
        this ModelBuilder modelBuilder, string schema = null)
            where TEventType : struct, Enum
            where TEntityType : struct, Enum
            where TPropertyType : struct, Enum =>
                modelBuilder
                    .ApplyConfiguration(new EventLogEntryConfiguration<TEventType, TEntityType, TPropertyType>(schema))
                    .ApplyConfiguration(new EntityLogEntryConfiguration<TEventType, TEntityType, TPropertyType>(schema))
                    .ApplyConfiguration(new BoolPropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType>(schema))
                    .ApplyConfiguration(new DateTimePropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType>(schema))
                    .ApplyConfiguration(new StringPropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType>(schema))
                    .ApplyConfiguration(new Int32PropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType>(schema))
                    .ApplyConfiguration(new DoublePropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType>(schema))
                    .ApplyConfiguration(new DecimalPropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType>(schema))
                    .ApplyConfiguration(new EventTypeDescriptionConfiguration<TEventType>(schema))
                    .ApplyConfiguration(new EventStatusDescriptionConfiguration(schema))
                    .ApplyConfiguration(new EntityTypeDescriptionConfiguration(schema))
                    .ApplyConfiguration(new PropertyTypeDescriptionConfiguration<TPropertyType>(schema));
}