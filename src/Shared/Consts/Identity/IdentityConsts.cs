namespace Finisher.Shared.Consts.Identity;

public static class IdentityConsts
{
    public const int MinPhoneLength = 10;
    public const int MaxPhoneLength = 13;
    public const string PhoneRegex = @"^(0|0098|\+98|)9(0[1-5]|[1 3]\d|2[0-2]|98)\d{7}$";

    public const int MaxFullNameLength = 100;

    public const int MinNationalCodeLength = 10;
    public const int MaxNationalCodeLength = 10;

    public const int MaxUserNameLength = 20;

    public const int MinPasswordLength = 8;
    public const int MaxPasswordLength = 50;

    public const int MaxIdLength = 256;

    public const int MinVerifyCodeLength = 4;
    public const int MaxVerifyCodeLength = 8;

    public const int MinVerifyCodeValue = 10000;
    public const int MaxVerifyCodeValue = 99999;
}
