namespace Finisher.Domain.ValueObjects;

public class TitleValueObject : ValueObject
{
    public TitleValueObject(string title)
    {
        Validate(title);

        Title = title;
    }

    private TitleValueObject() { }

    public string Title { get; private set; } = null!;

    public override string ToString() => Title;

    public static implicit operator string(TitleValueObject title) => title.Title;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Title;
    }

    private static void Validate(string title)
    {
        Guard.AgainstStringLengthAndNull(title, GlobalConsts.MinTitleLength, GlobalConsts.MaxTitleLength, nameof(title));
    }
}
