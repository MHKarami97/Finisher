using Finisher.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Finisher.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>, IAggregateRoot
{
    public string? NationalCode { get; init; }
    public string? FullName { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public bool IsActive { get; init; } = true;
}

public class ApplicationRole : IdentityRole<int>
{
    public ApplicationRole(string name) : base(name)
    {
    }
}

public class ApplicationUserClaim : IdentityUserClaim<int>;

public class ApplicationUserRole : IdentityUserRole<int>;

public class ApplicationUserLogin : IdentityUserLogin<int>;

public class ApplicationRoleClaim : IdentityRoleClaim<int>;

public class ApplicationUserToken : IdentityUserToken<int>;
