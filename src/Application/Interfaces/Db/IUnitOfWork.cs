using Microsoft.EntityFrameworkCore.Storage;

namespace Finisher.Application.Interfaces.Db;

public interface IUnitOfWork
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync(IDbContextTransaction transaction);
    Task RollbackTransactionAsync();
    Task<int> SaveChangesAsync();
}
