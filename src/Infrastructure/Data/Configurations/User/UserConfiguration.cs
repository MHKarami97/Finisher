using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Finisher.Infrastructure.Identity;
using Finisher.Shared.Consts.Identity;

namespace Finisher.Infrastructure.Data.Configurations.User;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable(nameof(ApplicationUser), StaticConfig.UserSchema);

        builder.Property(ua => ua.FullName)
            .HasMaxLength(IdentityConsts.MaxFullNameLength);

        builder.Property(ua => ua.NationalCode)
            .HasMaxLength(IdentityConsts.MaxNationalCodeLength);

        builder.Property(ua => ua.UserName)
            .HasMaxLength(IdentityConsts.MaxUserNameLength);

        builder.Property(ua => ua.NormalizedUserName)
            .HasMaxLength(IdentityConsts.MaxUserNameLength);

        builder.Property(ua => ua.PhoneNumber)
            .HasMaxLength(IdentityConsts.MaxPhoneLength);

        builder.HasIndex(b => b.PhoneNumber)
            .IsDescending(false)
            .IsUnique()
            .HasDatabaseName($"{StaticConfig.IndexPrefix}{StaticConfig.Separator}{StaticConfig.UserSchema}{nameof(ApplicationUser)}{StaticConfig.UserSchema}PhoneNumber");

        builder.HasIndex(b => b.NationalCode)
            .IsDescending(false)
            .IsUnique()
            .HasDatabaseName($"{StaticConfig.IndexPrefix}{StaticConfig.Separator}{StaticConfig.UserSchema}{nameof(ApplicationUser)}{StaticConfig.UserSchema}NationalCode");
    }
}

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable(nameof(ApplicationRole), StaticConfig.UserSchema);
    }
}

public class UserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.ToTable(nameof(ApplicationUserRole), StaticConfig.UserSchema);
    }
}

public class UserClaimConfiguration : IEntityTypeConfiguration<ApplicationRoleClaim>
{
    public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
    {
        builder.ToTable(nameof(ApplicationRoleClaim), StaticConfig.UserSchema);
    }
}

public class UserLoginConfiguration : IEntityTypeConfiguration<ApplicationUserLogin>
{
    public void Configure(EntityTypeBuilder<ApplicationUserLogin> builder)
    {
        builder.ToTable(nameof(ApplicationUserLogin), StaticConfig.UserSchema);
    }
}

public class RoleClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
{
    public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
    {
        builder.ToTable(nameof(ApplicationUserClaim), StaticConfig.UserSchema);
    }
}

public class UserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
{
    public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
    {
        builder.ToTable(nameof(ApplicationUserToken), StaticConfig.UserSchema);
    }
}
