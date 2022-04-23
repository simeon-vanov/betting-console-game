using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.InputHandlers.ActionParsers;

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

        if (amountParseResult.Failed)
            return Result<IAction>.Fail(amountParseResult.Errors);

        return Result<IAction>.Succeed(actionFactory.CreateDepositAction(amountParseResult.Value));
    }
}
