using AHSW.EventLog.Contracts.Models.Entities;
using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class EntityTypeDescriptionConfiguration :
    IEntityTypeConfiguration<EntityTypeDescription>
{
    public void Configure(EntityTypeBuilder<EntityTypeDescription> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.EntityTypeDescriptionsTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}