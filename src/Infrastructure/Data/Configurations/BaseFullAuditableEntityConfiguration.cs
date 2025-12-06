using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Finisher.Domain.Common;
using Finisher.Shared.Consts.Identity;

namespace Finisher.Infrastructure.Data.Configurations;

public abstract class BaseFullAuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseFullAuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(e => e.CreatedTime)
            .IsRequired();

        builder.Property(e => e.LastModifiedTime)
            .IsRequired(false);

        builder.Property(e => e.CreatedByUserId)
            .HasMaxLength(IdentityConsts.MaxIdLength)
            .IsRequired();

        builder.Property(e => e.LastModifiedByUserId)
            .HasMaxLength(IdentityConsts.MaxIdLength)
            .IsRequired(false);
    }
}

public abstract class BaseFullAuditableEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseFullAuditableEntity<TKey>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(e => e.CreatedTime)
            .IsRequired();

        builder.Property(e => e.LastModifiedTime)
            .IsRequired(false);

        builder.Property(e => e.CreatedByUserId)
            .HasMaxLength(IdentityConsts.MaxIdLength)
            .IsRequired();

        builder.Property(e => e.LastModifiedByUserId)
            .HasMaxLength(IdentityConsts.MaxIdLength)
            .IsRequired(false);
    }
}
