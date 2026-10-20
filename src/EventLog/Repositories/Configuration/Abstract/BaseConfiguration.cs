using System;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration.Abstract;

public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class
{
    private readonly string _eventLogSchema;

    protected BaseConfiguration(string schema)
    {
        _eventLogSchema = schema ?? EventLogPersistenceConstants.EventLogSchema;
    }

    protected abstract string EventLogTableName { get; }
    
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(EventLogTableName, _eventLogSchema);

        ConfigureEntity(builder);
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);

    protected static void MapEnumColumnType<T>(PropertyBuilder<T> propertyBuilder)
        where T : struct, Enum
    {
        var underlyingType = Enum.GetUnderlyingType(typeof(T));

        if (underlyingType == typeof(long))
            propertyBuilder.HasColumnType("bigint");
        
        if (underlyingType == typeof(int))
            propertyBuilder.HasColumnType("integer");
        
        if (underlyingType == typeof(short))
            propertyBuilder.HasColumnType("smallint");
        
        if (underlyingType == typeof(byte))
            propertyBuilder.HasColumnType("tinyint");
    }
}