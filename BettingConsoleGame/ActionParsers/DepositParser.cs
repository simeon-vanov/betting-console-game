using BettingConsoleGame.Application.Actions.Types;
using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.ActionParsers;

public class DepositParser : ActionWithAmountParser
{
    public override Result<IAction> Parse(string[] actionParameters)
    {
        var amountParseResult = ParseAmount(actionParameters);

        if (amountParseResult.ResultType == Domain.Enums.ResultType.Fail)
            return Result<IAction>.Failed(amountParseResult.Errors);

        return Result<IAction>.Succesful(new DepositAction(amountParseResult.ResultItem));
    }
}
