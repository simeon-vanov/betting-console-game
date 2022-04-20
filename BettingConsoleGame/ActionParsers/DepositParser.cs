using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame;

public class DepositParser : IConsoleActionParser
{
    public IAction Parse(string[] actionParameters)
    {
        if (actionParameters.Length > 2)
        {
            return new InvalidAction();
        }

        var amountString = actionParameters[1];

        if (!decimal.TryParse(amountString, out var amount))
        {
            return new InvalidAction();
        };

        return new DepositAction(new Money(amount, Currency.USDollar));
    }
}
