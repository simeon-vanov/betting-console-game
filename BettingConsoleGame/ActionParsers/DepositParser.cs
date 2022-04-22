using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.ActionParsers;

public class DepositParser : ActionWithAmountParser
{
    private readonly IActionFactory actionFactory;

    public DepositParser(IActionFactory actionFactory)
    {
        this.actionFactory = actionFactory;
    }

    public override Result<IAction> Parse(string[] actionParameters)
    {
        var amountParseResult = ParseAmount(actionParameters);

        if (amountParseResult.ResultType == Domain.Enums.ResultType.Fail)
            return Result<IAction>.Failed(amountParseResult.Errors);

        return Result<IAction>.Succesful(actionFactory.CreateDepositAction(amountParseResult.ResultItem));
    }
}
