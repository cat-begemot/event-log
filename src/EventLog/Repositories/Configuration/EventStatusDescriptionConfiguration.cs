using AHSW.EventLog.Contracts.Models.Entities;
using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class EventStatusDescriptionConfiguration :
    BaseConfiguration,
    IEntityTypeConfiguration<EventStatusDescription>
{
    public void Configure(EntityTypeBuilder<EventStatusDescription> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.EventStatusDescriptionsTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}