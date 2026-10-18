using System;
using AHSW.EventLog.Contracts.Models.Entities.PropertyLogEntries;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class BoolPropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType> :
    BaseConfiguration,
    IEntityTypeConfiguration<BoolPropertyLogEntry<TEventType, TEntityType, TPropertyType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    public void Configure(EntityTypeBuilder<BoolPropertyLogEntry<TEventType, TEntityType, TPropertyType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.BoolPropertyLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
        
        MapEnumColumnType(builder.Property(x => x.PropertyType));
        
        builder.HasIndex(x => x.PropertyType);
    }
}