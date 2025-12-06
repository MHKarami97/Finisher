using System.Linq.Expressions;
using System.Reflection;
using Finisher.Application.Interfaces.Db;
using Finisher.Domain.Common;
using Finisher.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Finisher.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<
    ApplicationUser,
    ApplicationRole,
    int,
    ApplicationUserClaim,
    ApplicationUserRole,
    ApplicationUserLogin,
    ApplicationRoleClaim,
    ApplicationUserToken>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .EnableServiceProviderCaching()
            .EnableThreadSafetyChecks()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .UseLazyLoadingProxies(false)
            .EnableSensitiveDataLogging(false)
            .UseLoggerFactory(LoggerFactory)
            .EnableDetailedErrors(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            .ConfigureWarnings(w =>
            {
                w.Ignore(RelationalEventId.PendingModelChangesWarning);
                w.Ignore(RelationalEventId.PendingModelChangesWarning);
            });
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.UseCollation(StaticConfig.Collate);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(BaseSoftDeleteEntity).IsAssignableFrom(entityType.ClrType) && !entityType.ClrType.IsAbstract)
            {
                builder.Entity(entityType.ClrType).HasQueryFilter(
                    CreateQueryFilterForSoftDelete(entityType.ClrType)
                );
            }
        }
    }

    private static LambdaExpression CreateQueryFilterForSoftDelete(Type entityType)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var property = Expression.Property(parameter, "IsDeleted");
        var filterExpression = Expression.Lambda(Expression.Not(property), parameter);

        return filterExpression;
    }

    private static readonly ILoggerFactory LoggerFactory =
        Microsoft.Extensions.Logging.LoggerFactory
            .Create(builder =>
                builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Debug)
                    .AddConsole());
}
