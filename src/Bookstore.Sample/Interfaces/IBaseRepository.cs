using AHSW.EventLog.Contracts.Models.Entities.Abstract;

namespace Bookstore.Sample.Interfaces;

internal interface IBaseRepository<in TEntity>
    where TEntity : PkEntity
{
    Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
}