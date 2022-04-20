using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;

namespace BettingConsoleGame.ActionParsers;

public class WithdrawParser : ActionWithAmountParser
{
    public override IAction Parse(string[] actionParameters)
    {
        return new WithdrawAction(ParseAmount(actionParameters));
    }
}
