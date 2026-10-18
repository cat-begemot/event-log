using System;
using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class EventTypeDescriptionConfiguration<TEventType> :
    BaseConfiguration,
    IEntityTypeConfiguration<EventTypeDescription>
        where TEventType : struct, Enum
{
    public void Configure(EntityTypeBuilder<EventTypeDescription> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.EventTypeDescriptionsTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}