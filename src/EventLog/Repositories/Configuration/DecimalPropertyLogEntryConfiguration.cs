using System;
using AHSW.EventLog.Contracts.Models.Entities.PropertyLogEntries;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class DecimalPropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType> :
    BaseConfiguration,
    IEntityTypeConfiguration<DecimalPropertyLogEntry<TEventType, TEntityType, TPropertyType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    public void Configure(EntityTypeBuilder<DecimalPropertyLogEntry<TEventType, TEntityType, TPropertyType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.DecimalPropertyLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
        
        MapEnumColumnType(builder.Property(x => x.PropertyType));
        
        builder.HasIndex(x => x.PropertyType);
    }
}