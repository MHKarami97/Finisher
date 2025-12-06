using Finisher.Shared.Extensions;

namespace Finisher.Domain.ValueObjects;

public class UrlValueObject : ValueObject
{
    public UrlValueObject(string url)
    {
        var urlSlug = url.ToSlug();

        Validate(urlSlug);

        Url = urlSlug;
    }

    protected UrlValueObject() { }

    public string Url { get; private set; } = null!;

    public override string ToString() => Url;

    public static implicit operator string(UrlValueObject url) => url.Url;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Url;
    }

    private static void Validate(string url)
    {
        Guard.AgainstStringLengthAndNull(url, GlobalConsts.MinUrlLength, GlobalConsts.MaxUrlLength, nameof(url));
        Guard.AgainstRegex(url, GlobalConsts.UrlRegex, nameof(url));
    }
}
