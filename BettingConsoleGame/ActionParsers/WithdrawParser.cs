using BettingConsoleGame.Domain.Entities.Action;
using BettingConsoleGame.Domain.Entities.Action.Types;

namespace BettingConsoleGame.ActionParsers;

public class WithdrawParser : ActionWithAmountParser
{
    public override IAction Parse(string[] actionParameters)
    {
        return new WithdrawAction(ParseAmount(actionParameters));
    }
}
