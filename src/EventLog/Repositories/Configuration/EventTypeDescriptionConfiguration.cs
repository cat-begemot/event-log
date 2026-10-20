using System;
using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class EventTypeDescriptionConfiguration<TEventType> :
    BaseConfiguration<EventTypeDescription>
        where TEventType : struct, Enum
{
    protected override string EventLogTableName => EventLogPersistenceConstants.EventTypeDescriptionsTableName;

    public EventTypeDescriptionConfiguration(string schema)
        : base(schema)
    {
    }

    protected override void ConfigureEntity(EntityTypeBuilder<EventTypeDescription> builder)
    {
    }
}