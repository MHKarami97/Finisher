using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Finisher.Domain.Constants;
using Finisher.Infrastructure.Identity;

namespace Finisher.Infrastructure.Data;

public static class InitializerExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var init = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await init.InitialiseAsync();

        await init.SeedAsync();
    }
}

public class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        await TrySeedRolesAsync();
        await TrySeedUserAsync();
    }

    private async Task TrySeedRolesAsync()
    {
        var roleNames = typeof(UserRoles)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi is { IsLiteral: true, IsInitOnly: false } && fi.FieldType == typeof(string))
            .Select(fi => fi.GetRawConstantValue() as string)
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .Distinct()
            .ToArray();

        foreach (var roleName in roleNames)
        {
            if (roleName != null && await roleManager.FindByNameAsync(roleName) is null)
            {
                await roleManager.CreateAsync(new ApplicationRole(roleName));
            }
        }
    }

    private async Task TrySeedUserAsync()
    {
        var user = new ApplicationUser
        {
            UserName = AdminUserConsts.AdminUserName,
            Email = AdminUserConsts.AdminEmail,
            PhoneNumber = AdminUserConsts.AdminPhoneNumber,
            FullName = AdminUserConsts.AdminFullName,
            IsActive = true,
            PhoneNumberConfirmed = true,
            EmailConfirmed = true
        };

        if (userManager.Users.All(u => u.PhoneNumber != user.PhoneNumber))
        {
            await userManager.CreateAsync(user, AdminUserConsts.AdminPassword);

            await userManager.AddToRolesAsync(user, [UserRoles.Administrator]);

            foreach (var permissionName in UserPermissions.GetPermissionsForAdmin())
            {
                await userManager.AddClaimAsync(user, new Claim("Permission", permissionName));
            }
        }
    }

    private async Task CreateDatabaseAsync()
    {
        var connectionString = context.Database.GetConnectionString();
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        var databaseName = connection.Database;

        await using var cmd = connection.CreateCommand();
        cmd.CommandText = $"""
                               IF DB_ID(N'{databaseName}') IS NULL
                               BEGIN
                                   CREATE DATABASE [{databaseName}] COLLATE {StaticConfig.Collate};
                               END
                           """;

        await cmd.ExecuteNonQueryAsync();
    }
}
