using BettingConsoleGame.Domain.Entities.Action;
using BettingConsoleGame.Domain.Entities.Action.Types;

namespace BettingConsoleGame.ActionParsers;

public class BetParser : ActionWithAmountParser
{
    public override IAction Parse(string[] actionParameters)
    {
        return new BetAction(ParseAmount(actionParameters));
    }
}
