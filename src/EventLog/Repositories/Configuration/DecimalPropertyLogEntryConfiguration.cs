using System;
using AHSW.EventLog.Contracts.Models.Entities.PropertyLogEntries;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class DecimalPropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType> :
    BaseConfiguration<DecimalPropertyLogEntry<TEventType, TEntityType, TPropertyType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    protected override string EventLogTableName => EventLogPersistenceConstants.DecimalPropertyLogTableName;

    public DecimalPropertyLogEntryConfiguration(string schema)
        : base(schema)
    {
    }

    protected override void ConfigureEntity(EntityTypeBuilder<DecimalPropertyLogEntry<TEventType, TEntityType, TPropertyType>> builder)
    {
        MapEnumColumnType(builder.Property(x => x.PropertyType));
        
        builder.HasIndex(x => x.PropertyType);
    }
}