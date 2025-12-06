using Finisher.Shared.Consts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Finisher.Web.Configurations;

internal static class SwaggerAuth
{
    public static void ConfigureSwaggerAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SwaggerAuthOptions>(
            configuration.GetSection(SwaggerAuthOptions.SectionName));

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = MainConsts.SwaggerCookie;
                options.DefaultChallengeScheme = MainConsts.SwaggerCookie;
            })
            .AddCookie(MainConsts.SwaggerCookie, options =>
            {
                options.LoginPath = MainConsts.SwaggerLogin;
                options.ReturnUrlParameter = string.Empty;
                options.AccessDeniedPath = MainConsts.SwaggerLogin;
                options.SlidingExpiration = true;
            });

        services.AddRazorPages();
    }

    public static void ConfigureSwaggerAuth(this WebApplication app)
    {
        var swaggerOptions = app.Services
            .GetRequiredService<IOptions<SwaggerAuthOptions>>()
            .Value;

        app.UseWhen(
            ctx => ctx.Request.Path.StartsWithSegments(MainConsts.Swagger, StringComparison.OrdinalIgnoreCase),
            branch =>
            {
                branch.Use(async (context, next) =>
                {
                    var user = context.User;
                    var identity = user.Identity;
                    if (identity is { IsAuthenticated: true })
                    {
                        var currentVersion = $"{swaggerOptions.Username}:{swaggerOptions.Password}";
                        var currentHash = Convert.ToBase64String(
                            System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(currentVersion)));

                        var claimHash = user.FindFirst(MainConsts.SwaggerConfigVersion)?.Value;

                        if (string.Equals(claimHash, currentHash, StringComparison.Ordinal))
                        {
                            await next();
                            return;
                        }
                    }

                    await context.SignOutAsync(MainConsts.SwaggerCookie);
                    await context.ChallengeAsync(MainConsts.SwaggerCookie);
                });
            });

        app.MapRazorPages();
    }
}

public sealed class SwaggerAuthOptions
{
    public const string SectionName = "SwaggerAuth";

    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
