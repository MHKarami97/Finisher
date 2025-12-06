using Finisher.Domain.Common;
using Finisher.Shared.Consts.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finisher.Infrastructure.Data.Configurations;

public abstract class BaseSoftDeleteEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseSoftDeleteEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(e => e.CreatedTime)
            .IsRequired();

        builder.Property(e => e.CreatedByUserId)
            .HasMaxLength(IdentityConsts.MaxIdLength)
            .IsRequired();

        builder.Property(e => e.DeleteByUserId)
            .IsRequired(false);

        builder.Property(e => e.DeletionTime)
            .IsRequired(false);

        builder.Property(e => e.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);
    }
}

public abstract class BaseSoftDeleteEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseSoftDeleteEntity<TKey>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(e => e.CreatedTime)
            .IsRequired();

        builder.Property(e => e.CreatedByUserId)
            .HasMaxLength(IdentityConsts.MaxIdLength)
            .IsRequired();

        builder.Property(e => e.DeleteByUserId)
            .IsRequired(false);

        builder.Property(e => e.DeletionTime)
            .IsRequired(false);

        builder.Property(e => e.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);
    }
}
