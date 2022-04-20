using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;
using BettingConsoleGame.Domain.ValueObjects;
using BettingConsoleGame.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingConsoleGame.ActionParsers;

public abstract class ActionWithAmountParser : IConsoleActionParser
{
    public abstract IAction Parse(string[] actionParameters);

    protected Money ParseAmount(string[] actionParameters)
    {
        if (actionParameters.Length > 2)
        {
            throw new InvalidActionParametersException("Action accepts only amount in the format 0.00");
        }

        var amountString = actionParameters[1];
        if (!decimal.TryParse(amountString, out var amount))
        {
            throw new InvalidActionParametersException("Amount must be in the format 0.00");
        }

        var decimalPlace = amountString.Split('.');

        if (decimalPlace.Length > 1 && decimalPlace[1].Length > 2)
        {
            throw new InvalidActionParametersException("Amount must be in the format 0.00");
        }

        return new Money(amount, Currency.USDollar);
    }
}
