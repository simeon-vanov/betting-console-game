using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.InputHandlers.ActionParsers;

public abstract class ActionWithAmountParser : IActionParser
{
    public abstract Result<IAction> Parse(string[] actionParameters);

    protected Result<Money> ParseAmount(string[] actionParameters)
    {
        if (actionParameters.Length > 2)
            return Result<Money>.Fail("Action accepts only amount as parameter.");

        var amountString = actionParameters[1];

        if (amountString.StartsWith(Currency.USDollar.Symbol))
            amountString = amountString.Substring(1);

        if (amountString.EndsWith(Currency.USDollar.Symbol))
            amountString = amountString.Substring(0, amountString.Length - 1);

        if (!decimal.TryParse(amountString, out var amount))
            return Result<Money>.Fail("Amount must be in the format 0.00.");

        if (amount <= 0)
            return Result<Money>.Fail("Amount must be positive number bigger than 0.");

        var decimalPlace = amountString.Split('.');

        if (decimalPlace.Length > 1 && decimalPlace[1].Length > 2)
            return Result<Money>.Fail("Amount must be in the format 0.00.");

        return Result<Money>.Succeed(new Money(amount, Currency.USDollar));
    }
}
