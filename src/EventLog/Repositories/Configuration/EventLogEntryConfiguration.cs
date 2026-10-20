using System;
using AHSW.EventLog.Contracts.Models.Entities;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class EventLogEntryConfiguration<TEventType, TEntityType, TPropertyType> :
    BaseConfiguration<EventLogEntry<TEventType, TEntityType, TPropertyType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    protected override string EventLogTableName => EventLogPersistenceConstants.EventLogTableName;

    public EventLogEntryConfiguration(string schema)
        : base(schema)
    {
    }

    protected override void ConfigureEntity(EntityTypeBuilder<EventLogEntry<TEventType, TEntityType, TPropertyType>> builder)
    {
        builder
            .HasMany(x => x.EntityLog)
            .WithOne(x => x.EventLogEntry)
            .HasForeignKey(x => x.EventLogEntryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        MapEnumColumnType(builder.Property(x => x.EventType));
        MapEnumColumnType(builder.Property(x => x.Status));
        
        builder.HasIndex(x => x.CreatedAt);
    }
}