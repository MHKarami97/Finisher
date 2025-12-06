using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Finisher.Domain.Common;
using Finisher.Shared.Consts.Identity;

namespace Finisher.Infrastructure.Data.Configurations;

public abstract class BaseAuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseAuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(e => e.CreatedTime)
            .IsRequired();

        builder.Property(e => e.CreatedByUserId)
            .HasMaxLength(IdentityConsts.MaxIdLength)
            .IsRequired();
    }
}

public abstract class BaseAuditableEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseAuditableEntity<TKey>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(e => e.CreatedTime)
            .IsRequired();

        builder.Property(e => e.CreatedByUserId)
            .HasMaxLength(IdentityConsts.MaxIdLength)
            .IsRequired();
    }
}
