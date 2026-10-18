using AHSW.EventLog.Contracts.Models.Entities.Abstract;
using Bookstore.Sample.DatabaseContext;
using Bookstore.Sample.Interfaces;

namespace Bookstore.Sample.Repository;

internal abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : PkEntity
{
    private readonly BookstoreDbContext _dbContext;

    protected BaseRepository(BookstoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddOrUpdateAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        if (IsNew())
            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        bool IsNew() => entity.Id == 0;
    }
}