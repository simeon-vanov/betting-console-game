using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;

namespace BettingConsoleGame.ActionParsers;

public class DepositParser : ActionWithAmountParser
{
    public override IAction Parse(string[] actionParameters)
    {
        return new DepositAction(ParseAmount(actionParameters));
    }
}
