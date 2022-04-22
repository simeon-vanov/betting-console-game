using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.InputHandlers.ActionParsers;

public class WithdrawParser : ActionWithAmountParser
{
    private readonly IActionFactory actionFactory;

    public WithdrawParser(IActionFactory actionFactory)
    {
        this.actionFactory = actionFactory;
    }

    public override Result<IAction> Parse(string[] actionParameters)
    {
        var amountParseResult = ParseAmount(actionParameters);

        if (amountParseResult.ResultType == Domain.Enums.ResultType.Fail)
            return Result<IAction>.Failed(amountParseResult.Errors);

        return Result<IAction>.Succesful(actionFactory.CreateWithdrawAction(amountParseResult.ResultItem));
    }
}
