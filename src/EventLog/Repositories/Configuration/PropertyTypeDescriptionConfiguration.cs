using System;
using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class PropertyTypeDescriptionConfiguration<TPropertyType> :
    BaseConfiguration<PropertyTypeDescription>
        where TPropertyType : struct, Enum
{
    protected override string EventLogTableName => EventLogPersistenceConstants.PropertyTypeDescriptionsTableName;

    public PropertyTypeDescriptionConfiguration(string schema)
        : base(schema)
    {
    }

    protected override void ConfigureEntity(EntityTypeBuilder<PropertyTypeDescription> builder)
    {
    }
}