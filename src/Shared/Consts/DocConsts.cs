namespace Finisher.Shared.Consts;

public static class DocConsts
{
    public const string SiteName = "Finisher";
    public const string Version = "v1";
    public const string StarterAddress = Slash + BaseAddress;
    public const string OpenApiJson = $"{Slash}{BaseAddress}{Slash}" + "{documentName}" + $"{Slash}{BaseAddress}.json";
    public const string SwaggerAddress = Slash + BaseAddress;
    public const string ScalarAddress = Slash + BaseAddress + Split + "api";
    public const string DocsLogin = "/DocsLogin";
    public const string DocsCookie = "DocsCookie";
    public const string DocsConfigVersion = "DocsConfigVersion";
    public const string DocsAccess = "DocsAccess";
    public const string FavIcon = "/swagger/favicon.ico";

    public const string SwaggerStyle =
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

    private const string BaseAddress = "docs";
    private const string Slash = "/";
    private const string Split = "-";
}
