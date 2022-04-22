using BettingConsoleGame.Application.Actions.Types;
using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Exceptions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.ActionParsers;

public class ExitParser : IConsoleActionParser
{
    public Result<IAction> Parse(string[] actionParameters)
    {
        if (actionParameters.Length > 1)
        {
            return Result<IAction>.Failed("Exit action does not accept any parameters.");
        }

        return Result<IAction>.Succesful(new ExitAction());
    }
}
