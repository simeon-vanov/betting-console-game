﻿using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;
using BettingConsoleGame.Interfaces;

namespace BettingConsoleGame.ActionParsers;

public abstract class ActionWithAmountParser : IConsoleActionParser
{
    public abstract Result<IAction> Parse(string[] actionParameters);

    protected Result<Money> ParseAmount(string[] actionParameters)
    {
        if (actionParameters.Length > 2)
            return Result<Money>.Failed("Action accepts only amount as parameter.");
        
        var amountString = actionParameters[1];

        if (amountString.StartsWith(Currency.USDollar.Symbol))
            amountString = amountString.Substring(1);

        if (amountString.EndsWith(Currency.USDollar.Symbol))
            amountString = amountString.Substring(0, amountString.Length -1);

        if (!decimal.TryParse(amountString, out var amount))
            return Result<Money>.Failed("Amount must be in the format 0.00.");

        if (amount <= 0)
            return Result<Money>.Failed("Amount must be positive number bigger than 0.");

        var decimalPlace = amountString.Split('.');

        if (decimalPlace.Length > 1 && decimalPlace[1].Length > 2)
            return Result<Money>.Failed("Amount must be in the format 0.00.");

        return Result<Money>.Succesful(new Money(amount, Currency.USDollar));
    }
}
