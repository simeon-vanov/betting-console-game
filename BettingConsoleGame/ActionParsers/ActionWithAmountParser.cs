using BettingConsoleGame.Application.Action.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;
using BettingConsoleGame.Exceptions;

namespace BettingConsoleGame.ActionParsers;

public abstract class ActionWithAmountParser : IConsoleActionParser
{
    public abstract IAction Parse(string[] actionParameters);

    protected Money ParseAmount(string[] actionParameters)
    {
        if (actionParameters.Length > 2)
            throw new InvalidActionParametersException("action accepts only amount in the format 0.00.");
        
        var amountString = actionParameters[1];

        if (amountString.StartsWith(Currency.USDollar.Symbol))
            amountString = amountString.Substring(1);

        if (amountString.EndsWith(Currency.USDollar.Symbol))
            amountString = amountString.Substring(0, amountString.Length -1);

        if (!decimal.TryParse(amountString, out var amount))
            throw new InvalidActionParametersException("amount must be in the format 0.00.");
        
        if (amount <= 0)
            throw new InvalidActionParametersException("amount must be a positive.");

        var decimalPlace = amountString.Split('.');

        if (decimalPlace.Length > 1 && decimalPlace[1].Length > 2)
            throw new InvalidActionParametersException("amount must be in the format 0.00.");

        return new Money(amount, Currency.USDollar);
    }
}
