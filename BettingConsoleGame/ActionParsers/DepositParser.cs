using BettingConsoleGame.Application.Action.Interfaces;
using BettingConsoleGame.Application.Action.Types;

namespace BettingConsoleGame.ActionParsers;

public class DepositParser : ActionWithAmountParser
{
    public override IAction Parse(string[] actionParameters)
    {
        return new DepositAction(ParseAmount(actionParameters));
    }
}
