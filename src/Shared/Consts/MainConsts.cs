namespace Finisher.Shared.Consts;

public static class MainConsts
{
    public const int Zero = 0;
    public const int EncodedIdLength = 10;
    public const string EncodedIdAlphabet = "mTHivO7hx3RAbr1f586SwjNnK2lgpcUVuG09BCtekZdJ4DYFPaWoMLQEsXIqyz";
    public const string DbConnectionName = "FinisherDb";
    public const string DefaultCulture = "fa-IR";
    public const int DbMaxRetry = 3;
    public const int DbMaxRetryDelayOnSecond = 10;
    public const int DbMaxBatchSize = 100;
    public static readonly int[] DbErrorToRetry = [1205]; // DeadLock
    public const int SqlTimeOutOnSecond = 30;
    public const string Swagger = "/swagger";
    public const string SwaggerLogin = "/SwaggerLogin";
    public const string SwaggerCookie = "SwaggerCookie";
    public const string SwaggerConfigVersion = "SwaggerConfigVersion";
    public const string SwaggerAccess = "SwaggerAccess";
    public const string MigrationTable = "Migration";
    public const string IdentityOptions = "IdentityOptions";
    public const string DevelopmentAdminVerifyCode = "11111";
    public const string JoinSeparator = "; ";
    public const string CacheProviderName = "InMemory";
    public const string CacheKeyPrefix = "EF_";
    public const int DbCallIfCacheDownOnMinute = 1;
}
