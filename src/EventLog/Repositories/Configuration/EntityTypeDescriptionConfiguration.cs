using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class EntityTypeDescriptionConfiguration :
    BaseConfiguration<EntityTypeDescription>
{
    protected override string EventLogTableName => EventLogPersistenceConstants.EntityTypeDescriptionsTableName;

    public EntityTypeDescriptionConfiguration(string schema)
        : base(schema)
    {
    }

    protected override void ConfigureEntity(EntityTypeBuilder<EntityTypeDescription> builder)
    {
    }
}