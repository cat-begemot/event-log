using System;
using System.Threading;
using System.Threading.Tasks;
using AHSW.EventLog.Contracts.Interfaces;
using AHSW.EventLog.Contracts.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AHSW.EventLog.Repositories;

public class ApplicationRepository<TDbContext> : IApplicationRepository
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    public ApplicationRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddOrUpdateEventLogAsync<TEventType, TEntityType, TPropertyType>(
        EventLogEntry<TEventType, TEntityType, TPropertyType> entity,
        CancellationToken cancellationToken = default)
            where TEventType : struct, Enum
            where TEntityType : struct, Enum
            where TPropertyType : struct, Enum
    {
        if (IsNew())
        {
            await _dbContext
                .Set<EventLogEntry<TEventType, TEntityType, TPropertyType>>()
                .AddAsync(entity, cancellationToken);
        }
        
        entity.CompletedAt = DateTime.UtcNow;
            
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        bool IsNew() => entity.Id == 0;
    }
    
    public object GetOriginalPropertyValue<TEntity>(TEntity entity, string propertyName)
        where TEntity : class =>
            _dbContext.Entry(entity).Property(propertyName).OriginalValue;
}