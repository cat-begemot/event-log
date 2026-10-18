using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration.Abstract;

public abstract class BaseConfiguration
{
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