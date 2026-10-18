using System;
using AHSW.EventLog.Contracts.Interfaces;
using AHSW.EventLog.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AHSW.EventLog.Extensions;

public static class ConfigureExtensions
{
    public static IServiceCollection AddEventLog<TDbContext, TEventType, TEntityType, TPropertyType>(
        this IServiceCollection services)
        where TDbContext : DbContext
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
    {
        services.AddScoped<
            IEventLogService<TEventType, TEntityType, TPropertyType>,
            EventLogService<TEventType, TEntityType, TPropertyType>>();
        
        services.AddScoped<IApplicationRepository, ApplicationRepository<TDbContext>>();
        
        return services;
    }
}