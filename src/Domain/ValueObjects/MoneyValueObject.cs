using Finisher.Domain.Enums;
using Finisher.Shared.Extensions;

namespace Finisher.Domain.ValueObjects;

public class MoneyValueObject : ValueObject
{
    public MoneyValueObject(int price, Currency currency = Currency.Toman)
    {
        Validate(price, currency);

        Price = price;
        Currency = currency;
    }

    protected MoneyValueObject() { }

    public int Price { get; private set; }
    public Currency Currency { get; private set; }

    public override string ToString() => Price.ToInvariantString();

    public static implicit operator string(MoneyValueObject money) => money.Price.ToInvariantString();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Price;
    }

    private static void Validate(int price, Currency currency)
    {
        Guard.AgainstNegativeOrZero(price, nameof(price));
        Guard.AgainstInvalidEnum(currency, nameof(currency));
    }
}
