using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;
using BettingConsoleGame.Domain.ValueObjects;
using BettingConsoleGame.Exceptions;

namespace BettingConsoleGame.ActionParsers;

public class DepositParser : ActionWithAmountParser
{
    public override IAction Parse(string[] actionParameters)
    {
        return new DepositAction(ParseAmount(actionParameters));
    }
}
