using EFCoreSecondLevelCacheInterceptor;
using Finisher.Application.Interfaces;
using Finisher.Application.Interfaces.Db;
using Finisher.Application.Interfaces.Security;
using Finisher.Application.Interfaces.User;
using Finisher.Domain.Interfaces.Db;
using Finisher.Infrastructure.Data;
using Finisher.Infrastructure.Data.Interceptors;
using Finisher.Infrastructure.Database;
using Finisher.Infrastructure.File;
using Finisher.Infrastructure.Identity;
using Finisher.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Finisher.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        }

        var connectionString = builder.Configuration.GetConnectionString(MainConsts.DbConnectionName);
        Guard.Against.Null(connectionString, message: Messages.ConnectionStringNotFound);

        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        builder.Services.AddScoped<IFileStorageService, FileStorageService>();
        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        builder.Services.AddSingleton<IPhoneSignInCodeService, PhoneSignInCodeService>();
        builder.Services.AddScoped<IUniqueConstraintChecker, SqlServerUniqueConstraintChecker>();
        builder.Services.AddScoped<ApplicationDbContextInitializer>();
        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddTransient<IIdentityService, IdentityService>();
        builder.Services.AddTransient(typeof(IRepository<,>), typeof(BaseRepository<,>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        AddRepository(builder);

        builder.Services.AddEFSecondLevelCache(options =>
            options
                .UseEasyCachingCoreProvider(MainConsts.CacheProviderName, isHybridCache: false)
                .ConfigureLogging(true)
                .UseCacheKeyPrefix(MainConsts.CacheKeyPrefix)
                .UseDbCallsIfCachingProviderIsDown(TimeSpan.FromMinutes(MainConsts.DbCallIfCacheDownOnMinute))
        );

        builder.Services.AddDbContextPool<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString,
                s =>
                {
                    s.UseRelationalNulls(false);
                    s.MaxBatchSize(MainConsts.DbMaxBatchSize);
                    s.CommandTimeout(MainConsts.SqlTimeOutOnSecond);
                    s.MigrationsHistoryTable(MainConsts.MigrationTable, StaticConfig.ConfigSchema);
                    s.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    s.EnableRetryOnFailure(
                        maxRetryCount: MainConsts.DbMaxRetry,
                        maxRetryDelay: TimeSpan.FromSeconds(MainConsts.DbMaxRetryDelayOnSecond),
                        errorNumbersToAdd: MainConsts.DbErrorToRetry);
                }
            );
        });

        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        builder.Services.Configure<IdentityOptions>(builder.Configuration.GetSection(MainConsts.IdentityOptions));

        builder.Services.AddAuthorizationBuilder();

        builder.Services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();
    }

    private static void AddRepository(IHostApplicationBuilder builder)
    {
    }
}
