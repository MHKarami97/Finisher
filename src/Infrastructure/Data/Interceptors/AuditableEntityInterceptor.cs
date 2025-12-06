using Finisher.Application.Interfaces.User;
using Finisher.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Hosting;

namespace Finisher.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor(
    IUser user,
    TimeProvider dateTime,
    IHostEnvironment environment) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        AddOrUpdateEntities(eventData.Context);
        DeleteEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        AddOrUpdateEntities(eventData.Context);
        DeleteEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void AddOrUpdateEntities(DbContext? context)
    {
        if (context == null)
        {
            return;
        }

        foreach (var entry in context.ChangeTracker.Entries())
        {
            var entity = entry.Entity;
            if (entity is not (BaseAuditableEntity or BaseFullAuditableEntity or BaseSoftDeleteEntity))
            {
                continue;
            }

            if (entry.State is EntityState.Added or EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                var utcNow = dateTime.GetUtcNow();

                switch (entry.State)
                {
                    case EntityState.Added:
                        switch (entity)
                        {
                            case BaseAuditableEntity auditable:
                                if (!IsSeedData(auditable))
                                {
                                    auditable.SetCreatedByUserId(user.Id);
                                    auditable.SetCreatedTime(utcNow);
                                }

                                break;

                            case BaseFullAuditableEntity auditableFull:
                                if (!IsSeedData(auditableFull))
                                {
                                    auditableFull.SetCreatedByUserId(user.Id);
                                    auditableFull.SetCreatedTime(utcNow);
                                }

                                break;

                            case BaseSoftDeleteEntity auditableFull:
                                if (!IsSeedData(auditableFull))
                                {
                                    auditableFull.SetCreatedByUserId(user.Id);
                                    auditableFull.SetCreatedTime(utcNow);
                                }

                                break;
                        }

                        break;

                    case EntityState.Modified:
                        {
                            if (entity is BaseFullAuditableEntity auditableFull)
                            {
                                auditableFull.SetLastModifiedByUserId(user.Id);
                                auditableFull.SetLastModifiedTime(utcNow);
                            }

                            break;
                        }
                }
            }
        }
    }

    private void DeleteEntities(DbContext? context)
    {
        if (context == null)
        {
            return;
        }

        foreach (var entry in context.ChangeTracker.Entries<BaseSoftDeleteEntity>())
        {
            if (entry.State is EntityState.Deleted)
            {
                var utcNow = dateTime.GetUtcNow();

                entry.Entity.SetDeleteByUserId(user.Id);
                entry.Entity.SetDeletionTime(utcNow);
                entry.Entity.SetIsDeleted(true);

                entry.State = EntityState.Modified;
            }
        }
    }

    private bool IsSeedData(BaseFullAuditableEntity entity)
    {
        return environment.IsDevelopment() && entity.CreatedByUserId != null;
    }

    private bool IsSeedData(BaseAuditableEntity entity)
    {
        return environment.IsDevelopment() && entity.CreatedByUserId != null;
    }

    private bool IsSeedData(BaseSoftDeleteEntity entity)
    {
        return environment.IsDevelopment() && entity.CreatedByUserId != null;
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
