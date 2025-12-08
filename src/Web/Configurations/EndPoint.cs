using System.Text.Json;
using System.Text.Json.Serialization;
using FastEndpoints.Swagger;
using Finisher.Shared.Consts.Identity;
using Finisher.Web.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Namotion.Reflection;

namespace Finisher.Web.Configurations;

internal static class EndPoint
{
    public static void ConfigureEndPoint(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.AddHttpContextAccessor();
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
                    s.DocumentName = StaticDoc.SiteName;
                    s.Title = StaticDoc.SiteName;
                    s.Description = string.Empty;
                    s.Version = StaticDoc.Version;
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

        app.UseResponseCaching()
            .UseCustomExceptionHandler()
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
            }).UseSwaggerGen(uiConfig: a =>
            {
                a.PersistAuthorization = true;
                a.EnableTryItOut = true;
                a.DocumentTitle = StaticDoc.SiteName;
                a.CustomInlineStyles =
                    ".swagger-ui .topbar{background-color:#6f42c1}" +
                    ".swagger-ui .btn.authorize{background-color:#6f42c1;border-color:#6f42c1}" +
                    ".swagger-ui .btn.authorize svg{fill:white}" +
                    ".swagger-ui .opblock.opblock-get .opblock-summary-method,.swagger-ui .opblock.opblock-post .opblock-summary-method{background-color:#6f42c1}" +
                    ".swagger-ui .info h1,.swagger-ui .info h2{color:#6f42c1}" +
                    ".version-stamp{display:none}" +
                    ".models{display:none}" +
                    ".swagger-ui .topbar .download-url-wrapper .select-label select{border: 2px solid #6f42c1}" +
                    ".swagger-ui .btn.authorize{color:#3b4151}" +
                    ".swagger-ui .topbar a{display:none}" +
                    ".swagger-ui .info .title small.version-stamp{display:none}" +
                    ".swagger-ui section.models.is-open h4{display:none}" +
                    ".swagger-ui .opblock.opblock-post .opblock-summary-method{background-color:#49cc90}" +
                    ".swagger-ui .opblock.opblock-get .opblock-summary-method{background-color:#61affe}";
            });
    }
}
