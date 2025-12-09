using System.Text.Json;
using System.Text.Json.Serialization;
using FastEndpoints.Swagger;
using Finisher.Shared.Consts;
using Finisher.Shared.Consts.Identity;
using Finisher.Web.Extensions;
using Finisher.Web.Handler;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Namotion.Reflection;
using Scalar.AspNetCore;

namespace Finisher.Web.Configurations;

internal static class EndPoint
{
    public static void ConfigureEndPoint(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.AddHttpContextAccessor();
        services.AddOpenApi();
        services.AddEndpointsApiExplorer();
        services.AddOutputCache();

        services.AddFastEndpoints(o =>
            {
                o.DisableAutoDiscovery = false;
                o.IncludeAbstractValidators = true;
            })
            .AddResponseCaching()
            .AddFluentValidationAutoValidation()
            .SwaggerDocument(o =>
            {
                o.ValidateNullability();
                o.DocumentSettings = s =>
                {
                    s.DocumentName = DocConsts.Version;
                    s.Title = DocConsts.SiteName;
                    s.Description = string.Empty;
                    s.Version = DocConsts.Version;
                    s.AllowNullableBodyParameters = true;
                };
                o.ShortSchemaNames = true;
                o.EnableJWTBearerAuth = true;
            });
    }

    public static void ConfigureEndPoint(this WebApplication app)
    {
        app.UseOutputCache();
        app.Map("/", () => Results.Redirect($"/{ApiRoutes.Api}"));
        var isDevelopment = app.Environment.IsDevelopment();

        app.UseResponseCaching()
            .UseCustomExceptionHandler(showExceptionDetail: isDevelopment)
            .UseFastEndpoints(c =>
            {
                c.Endpoints.RoutePrefix = ApiRoutes.Api;
                c.Versioning.Prefix = ApiRoutes.VersioningPrefix;
                c.Binding.UsePropertyNamingPolicy = true;
                c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                c.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
                c.Security.PermissionsClaimType = ClaimType.Policy;
                c.Security.RoleClaimType = ClaimType.Role;
                c.Security.NameClaimType = ClaimType.Name;
                c.Endpoints.ShortNames = true;
            }).UseSwaggerGen(g =>
            {
                g.Path = DocConsts.OpenApiJson;
            }, uiConfig: a =>
            {
                a.DocumentPath = DocConsts.OpenApiJson;
                a.Path = DocConsts.SwaggerAddress;
                a.PersistAuthorization = true;
                a.EnableTryItOut = true;
                a.DocumentTitle = DocConsts.SiteName;
                a.CustomInlineStyles = DocConsts.SwaggerStyle;
            });

        app.MapScalarApiReference(DocConsts.ScalarAddress, o =>
        {
            o.Title = DocConsts.SiteName;
            o.Favicon = DocConsts.FavIcon;
            o.Theme = ScalarTheme.Moon;
            o.Layout = ScalarLayout.Modern;
            o.ShowDeveloperTools = DeveloperToolsVisibility.Localhost;
            o.WithOpenApiRoutePattern(DocConsts.OpenApiJson);
            o.AddPreferredSecuritySchemes(CustomJwt.AuthSchemaName);
            o.AddHttpAuthentication(CustomJwt.AuthSchemaName, _ => { });
        });
    }
}
