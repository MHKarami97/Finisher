using Finisher.Shared.Consts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Finisher.Web.Configurations;

internal static class SwaggerAuth
{
    public static void ConfigureSwaggerAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SwaggerAuthOptions>(
            configuration.GetSection(SwaggerAuthOptions.SectionName));

        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = MainConsts.SwaggerLogin;
                options.AccessDeniedPath = MainConsts.SwaggerLogin;
                options.SlidingExpiration = true;
            });

        services.AddAuthorization();

        services.AddRazorPages();
    }

    public static void ConfigureSwaggerAuth(this WebApplication app)
    {
        app.UseWhen(
            ctx => ctx.Request.Path.StartsWithSegments(MainConsts.Swagger, StringComparison.OrdinalIgnoreCase),
            branch =>
            {
                branch.Use(async (context, next) =>
                {
                    var identity = context.User.Identity;
                    if (identity is { IsAuthenticated: true })
                    {
                        await next();
                        return;
                    }

                    await context.ChallengeAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                });
            });

        app.MapRazorPages();
    }
}

internal sealed class SwaggerAuthOptions
{
    public const string SectionName = "SwaggerAuth";

    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
