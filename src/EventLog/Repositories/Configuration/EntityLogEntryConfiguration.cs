using System;
using AHSW.EventLog.Contracts.Models.Entities;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class EntityLogEntryConfiguration<TEventType, TEntityType, TPropertyType> :
    BaseConfiguration,
    IEntityTypeConfiguration<EntityLogEntry<TEventType, TEntityType, TPropertyType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    public void Configure(EntityTypeBuilder<EntityLogEntry<TEventType, TEntityType, TPropertyType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.EntityLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
        
        builder
            .HasMany(x => x.BoolPropertyLog)
            .WithOne(x => x.EntityLogEntry)
            .HasForeignKey(x => x.EntityLogEntryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasMany(x => x.DateTimePropertyLog)
            .WithOne(x => x.EntityLogEntry)
            .HasForeignKey(x => x.EntityLogEntryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasMany(x => x.StringPropertyLog)
            .WithOne(x => x.EntityLogEntry)
            .HasForeignKey(x => x.EntityLogEntryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasMany(x => x.Int32PropertyLog)
            .WithOne(x => x.EntityLogEntry)
            .HasForeignKey(x => x.EntityLogEntryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasMany(x => x.DoublePropertyLog)
            .WithOne(x => x.EntityLogEntry)
            .HasForeignKey(x => x.EntityLogEntryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasMany(x => x.DecimalPropertyLog)
            .WithOne(x => x.EntityLogEntry)
            .HasForeignKey(x => x.EntityLogEntryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        MapEnumColumnType(builder.Property(x => x.EntityType));
        MapEnumColumnType(builder.Property(x => x.ActionType));
        
        builder.HasIndex(x => x.EntityId);
    }
}