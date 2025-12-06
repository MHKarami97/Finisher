using Microsoft.AspNetCore.Authorization;
using Finisher.Shared.Consts.Identity;

namespace Finisher.Web.Configurations;

public static class Authorization
{
    public static void ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(UserPolicies.CanView, policy =>
                policy.RequireRole(UserRoles.User, UserRoles.Moderator, UserRoles.Administrator)
                    .RequireClaim(ClaimType.Permission, UserPermissions.CanView))
            .AddPolicy(UserPolicies.CanIdentity, policy =>
                policy.RequireRole(UserRoles.User, UserRoles.Moderator, UserRoles.Administrator)
                    .RequireClaim(ClaimType.Permission, UserPermissions.CanIdentity))
            .AddPolicy(UserPolicies.CanCreate, policy =>
                policy.RequireRole(UserRoles.Moderator, UserRoles.Administrator)
                    .RequireClaim(ClaimType.Permission, UserPermissions.CanCreate))
            .AddPolicy(UserPolicies.CanUpdate, policy =>
                policy.RequireRole(UserRoles.Moderator, UserRoles.Administrator)
                    .RequireClaim(ClaimType.Permission, UserPermissions.CanUpdate))
            .AddPolicy(UserPolicies.CanDelete, policy =>
                policy.RequireRole(UserRoles.Administrator)
                    .RequireClaim(ClaimType.Permission, UserPermissions.CanDelete))
            .AddPolicy(UserPolicies.CanPurge, policy =>
                policy.RequireRole(UserRoles.Administrator)
                    .RequireClaim(ClaimType.Permission, UserPermissions.CanDelete))
            .SetDefaultPolicy(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(ClaimType.Role)
                .Build());
    }

    public static void ConfigureAuthorization(this WebApplication app)
    {
        app.UseAuthorization();
    }
}
