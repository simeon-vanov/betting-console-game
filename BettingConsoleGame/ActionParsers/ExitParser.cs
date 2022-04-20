using BettingConsoleGame.Domain.Entities.Action;
using BettingConsoleGame.Domain.Entities.Action.Types;
using BettingConsoleGame.Exceptions;

namespace BettingConsoleGame.ActionParsers;

public class ExitParser : IConsoleActionParser
{
    public IAction Parse(string[] actionParameters)
    {
        if (actionParameters.Length > 1)
        {
            throw new InvalidActionParametersException("exit does not accept any parameters.");
        }

        return new ExitAction();
    }
}
