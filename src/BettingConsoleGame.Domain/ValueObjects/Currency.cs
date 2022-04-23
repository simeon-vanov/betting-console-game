namespace BettingConsoleGame.Domain.ValueObjects;

public record Currency(string Abbreviation, string Symbol)
{
    public static Currency USDollar => new Currency("USD", "$");
}
