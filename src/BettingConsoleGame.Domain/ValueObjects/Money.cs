namespace BettingConsoleGame.Domain.ValueObjects;

public record Money
{
    public static Money ZeroDollars => new Money(0, Currency.USDollar);
    public static Money OneDollar => new Money(1, Currency.USDollar);
    public static Money TenDollars => new Money(10, Currency.USDollar);

    public static Money Dollars(decimal amount) => new Money(amount, Currency.USDollar);

    public Money(decimal amount, Currency currency)
    {
        this.Amount = decimal.Round(amount, 2);
        this.Currency = currency;
    }

    public decimal Amount { get; }

    public Currency Currency { get; }

    public override string ToString()
    {
        return $"{this.Currency.Symbol}{this.Amount}";
    }

    public static Money operator +(Money money1, Money money2)
    {
        if (money1.Currency != money2.Currency)
        {
            throw new InvalidOperationException("Amount of different currencies cannot be sumed");
        }

        return new Money(money1.Amount + money2.Amount, money1.Currency);
    }

    public static Money operator -(Money money1, Money money2)
    {
        if (money1.Currency != money2.Currency)
        {
            throw new InvalidOperationException("Amount of different currencies cannot be sumed");
        }

        return new Money(money1.Amount - money2.Amount, money1.Currency);
    }

    public static bool operator >(Money money, Money money2)
    {
        return (money?.Amount ?? 0) > (money2?.Amount ?? 0);
    }

    public static bool operator <(Money money, Money money2)
    {
        return (money?.Amount ?? 0) < (money2?.Amount ?? 0);
    }

    public static Money operator *(Money money, int multiplier)
    {
        return new Money(money.Amount * multiplier, money.Currency);
    }

    public static Money operator /(Money money, int divider)
    {
        return new Money(money.Amount / divider, money.Currency);
    }

    public static Money operator *(Money money, double multiplier)
    {
        return new Money(money.Amount * (decimal)multiplier, money.Currency);
    }

    public static Money operator /(Money money, double divider)
    {
        return new Money(money.Amount / (decimal)divider, money.Currency);
    }

    public static bool operator >(Money money, decimal amount)
    {
        return (money?.Amount ?? 0) > amount;
    }

    public static bool operator >=(Money money, decimal amount)
    {
        return (money?.Amount ?? 0) >= amount;
    }

    public static bool operator <=(Money money, decimal amount)
    {
        return (money?.Amount ?? 0) >= amount;
    }

    public static bool operator <(Money money, decimal amount)
    {
        return (money?.Amount ?? 0) < amount;
    }

    public static bool operator ==(Money money, decimal amount)
    {
        return (money?.Amount ?? 0) == amount;
    }

    public static bool operator !=(Money money, decimal amount)
    {
        return (money?.Amount ?? 0) != amount;
    }
}
