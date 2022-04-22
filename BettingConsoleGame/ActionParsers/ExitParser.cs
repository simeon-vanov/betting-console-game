using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;
using BettingConsoleGame.Interfaces;

namespace BettingConsoleGame.ActionParsers;

public class ExitParser : IConsoleActionParser
{
    private readonly IActionFactory actionFactory;

    public ExitParser(IActionFactory actionFactory)
    {
        this.actionFactory = actionFactory;
    }

    public Result<IAction> Parse(string[] actionParameters)
    {
        if (actionParameters.Length > 1)
        {
            return Result<IAction>.Failed("Exit action does not accept any parameters.");
        }

        return Result<IAction>.Succesful(actionFactory.CreateExitAction());
    }
}
