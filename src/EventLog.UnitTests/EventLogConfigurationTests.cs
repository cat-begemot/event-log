using AHSW.EventLog.Contracts.Models.Entities;
using AHSW.EventLog.Repositories.Constants;
using AHSW.EventLog.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EventLog.UnitTests;

public class EventLogConfigurationTests
{
    private enum TestType { Value }

    [TestCase(null)]
    [TestCase("audit")]
    public void ApplyEventLogConfigurations_MapsAllTablesToSelectedSchema(string? schema)
    {
        var builder = new ModelBuilder(new ConventionSet());

        var result = builder.ApplyEventLogConfigurations<TestType, TestType, TestType>(schema);

        Assert.That(result, Is.SameAs(builder));
        Assert.That(builder.Model.GetEntityTypes().Select(entity => entity.GetTableName()),
            Is.EquivalentTo(new[]
            {
                EventLogPersistenceConstants.EventLogTableName,
                EventLogPersistenceConstants.EntityLogTableName,
                EventLogPersistenceConstants.BoolPropertyLogTableName,
                EventLogPersistenceConstants.DateTimePropertyLogTableName,
                EventLogPersistenceConstants.StringPropertyLogTableName,
                EventLogPersistenceConstants.Int32PropertyLogTableName,
                EventLogPersistenceConstants.DoublePropertyLogTableName,
                EventLogPersistenceConstants.DecimalPropertyLogTableName,
                EventLogPersistenceConstants.EventTypeDescriptionsTableName,
                EventLogPersistenceConstants.EventStatusDescriptionsTableName,
                EventLogPersistenceConstants.EntityTypeDescriptionsTableName,
                EventLogPersistenceConstants.PropertyTypeDescriptionsTableName
            }));
        Assert.That(builder.Model.GetEntityTypes().Select(entity => entity.GetSchema()),
            Is.All.EqualTo(schema ?? EventLogPersistenceConstants.EventLogSchema));
    }

    [Test]
    public void ApplyEventLogConfigurations_WithoutSchema_PreservesEntitySettings()
    {
        var builder = new ModelBuilder(new ConventionSet());

        builder.ApplyEventLogConfigurations<TestType, TestType, TestType>();

        var entity = builder.Model.FindEntityType(typeof(EventLogEntry<TestType, TestType, TestType>))!;
        Assert.That(entity.GetSchema(), Is.EqualTo(EventLogPersistenceConstants.EventLogSchema));
        Assert.That(entity.FindProperty("EventType")!.GetColumnType(), Is.EqualTo("integer"));
        Assert.That(entity.GetIndexes().SelectMany(index => index.Properties).Select(property => property.Name),
            Does.Contain("CreatedAt"));
        Assert.That(entity.GetReferencingForeignKeys().Single().DeleteBehavior,
            Is.EqualTo(DeleteBehavior.Restrict));
    }
}
