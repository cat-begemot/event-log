using System;
using AHSW.EventLog.Contracts.Models.Entities.PropertyLogEntries;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class BoolPropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType> :
    BaseConfiguration<BoolPropertyLogEntry<TEventType, TEntityType, TPropertyType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    protected override string EventLogTableName => EventLogPersistenceConstants.BoolPropertyLogTableName;

    public BoolPropertyLogEntryConfiguration(string schema)
        : base(schema)
    {
    }

    protected override void ConfigureEntity(EntityTypeBuilder<BoolPropertyLogEntry<TEventType, TEntityType, TPropertyType>> builder)
    {
        MapEnumColumnType(builder.Property(x => x.PropertyType));
        
        builder.HasIndex(x => x.PropertyType);
    }
}