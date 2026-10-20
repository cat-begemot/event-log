using System;
using AHSW.EventLog.Contracts.Models.Entities.PropertyLogEntries;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class Int32PropertyLogEntryConfiguration<TEventType,TEntityType, TPropertyType> :
    BaseConfiguration<Int32PropertyLogEntry<TEventType, TEntityType, TPropertyType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    protected override string EventLogTableName => EventLogPersistenceConstants.Int32PropertyLogTableName;

    public Int32PropertyLogEntryConfiguration(string schema)
        : base(schema)
    {
    }

    protected override void ConfigureEntity(EntityTypeBuilder<Int32PropertyLogEntry<TEventType, TEntityType, TPropertyType>> builder)
    {
        MapEnumColumnType(builder.Property(x => x.PropertyType));
        
        builder.HasIndex(x => x.PropertyType);
    }
}