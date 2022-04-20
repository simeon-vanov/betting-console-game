﻿using BettingConsoleGame.Domain.Entities.Action;
using BettingConsoleGame.Domain.Entities.Action.Types;

namespace BettingConsoleGame.ActionParsers;

public class DepositParser : ActionWithAmountParser
{
    public override IAction Parse(string[] actionParameters)
    {
        return new DepositAction(ParseAmount(actionParameters));
    }
}
