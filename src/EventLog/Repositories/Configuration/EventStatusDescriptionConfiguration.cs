using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class EventStatusDescriptionConfiguration :
    BaseConfiguration<EventStatusDescription>
{
    protected override string EventLogTableName => EventLogPersistenceConstants.EventStatusDescriptionsTableName;

    public EventStatusDescriptionConfiguration(string schema)
        : base(schema)
    {
    }

    protected override void ConfigureEntity(EntityTypeBuilder<EventStatusDescription> builder)
    {
    }
}