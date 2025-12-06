using System.Globalization;
using OwaspHeaders.Core.Enums;
using OwaspHeaders.Core.Extensions;

namespace Finisher.Web.Configurations;

internal static class SecureHeaders
{
    public static OwaspHeaders.Core.Models.SecureHeadersMiddlewareConfiguration SecureHeadersConfiguration(IConfiguration configuration)
    {
        return SecureHeadersMiddlewareBuilder
            .CreateBuilder()
            .UseContentTypeOptions()
            .UseCacheControl(false, int.Parse(configuration["Cache:DurationOnSecond"]!, CultureInfo.InvariantCulture), false, false)
            .UseCrossOriginResourcePolicy()
            .UseXssProtection()
            .UseXFrameOptions()
            .UseReferrerPolicy()
            .UseContentSecurityPolicy()
            .UseHsts(31536000, true)
            .UseReferrerPolicy(ReferrerPolicyOptions.sameOrigin)
            .UsePermittedCrossDomainPolicies(XPermittedCrossDomainOptionValue.masterOnly)
            .Build();
    }
}
