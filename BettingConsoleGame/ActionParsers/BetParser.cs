using BettingConsoleGame.Application.Action.Interfaces;
using BettingConsoleGame.Application.Action.Types;

namespace BettingConsoleGame.ActionParsers;

public class BetParser : ActionWithAmountParser
{
    public override IAction Parse(string[] actionParameters)
    {
        return new BetAction(ParseAmount(actionParameters));
    }
}
