using System.Globalization;
using Finisher.Shared.Consts;

namespace Finisher.Web.Configurations;

internal static class Culture
{
    internal const string DefaultCulture = MainConsts.DefaultCulture;

    public static void ConfigureCulture()
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(DefaultCulture);
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(DefaultCulture);
    }
}
