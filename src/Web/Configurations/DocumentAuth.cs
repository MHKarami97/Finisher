using System.Globalization;
using Finisher.Shared.Consts;
using Finisher.Shared.Resources;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Finisher.Web.Configurations;

internal static class DocumentAuth
{
    public static void ConfigureDocumentAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DocsAuthOptions>(
            configuration.GetSection(DocsAuthOptions.SectionName));

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = DocConsts.DocsCookie;
                options.DefaultChallengeScheme = DocConsts.DocsCookie;
            })
            .AddCookie(DocConsts.DocsCookie, options =>
            {
                options.LoginPath = DocConsts.DocsLogin;
                options.ReturnUrlParameter = string.Empty;
                options.AccessDeniedPath = DocConsts.DocsLogin;
                options.SlidingExpiration = true;
            });

        services.AddRazorPages();
    }

    public static void ConfigureDocumentAuth(this WebApplication app)
    {
        var authOptions = app.Services
            .GetRequiredService<IOptions<DocsAuthOptions>>()
            .Value;

        var timeProvider = app.Services.GetRequiredService<TimeProvider>();

        app.UseWhen(
            ctx => ctx.Request.Path.StartsWithSegments(DocConsts.StarterAddress, StringComparison.OrdinalIgnoreCase),
            branch =>
            {
                branch.Use(async (context, next) =>
                {
                    var user = context.User;
                    var identity = user.Identity;
                    if (identity is { IsAuthenticated: true })
                    {
                        try
                        {
                            var currentVersion = $"{authOptions.Username}:{authOptions.Password}";

                            if (authOptions.ExpireEveryWeek)
                            {
                                var now = timeProvider.GetUtcNow();
                                var utcDate = now.UtcDateTime;

                                var isoWeek = ISOWeek.GetWeekOfYear(utcDate);
                                var isoYear = ISOWeek.GetYear(utcDate);

                                var weekId = $"{isoYear:D4}-W{isoWeek:D2}";
                                currentVersion += $":{weekId}";
                            }

                            var currentHash = Convert.ToBase64String(
                                System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(currentVersion)));

                            var claimHash = user.FindFirst(DocConsts.DocsConfigVersion)?.Value;

                            if (string.Equals(claimHash, currentHash, StringComparison.Ordinal))
                            {
                                await next();
                                return;
                            }
                        }
                        catch (Exception e)
                        {
                            app.Logger.LogError(e, Messages.UnauthorizedExeption);
                        }
                    }

                    await context.SignOutAsync(DocConsts.DocsCookie);
                    await context.ChallengeAsync(DocConsts.DocsCookie);
                });
            });

        app.MapRazorPages();
    }
}

public sealed class DocsAuthOptions
{
    public const string SectionName = "DocsAuth";

    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public bool ExpireEveryWeek { get; init; }
}
