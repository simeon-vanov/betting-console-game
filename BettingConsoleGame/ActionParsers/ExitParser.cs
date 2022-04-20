using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;
using BettingConsoleGame.Exceptions;

namespace BettingConsoleGame.Parsers;

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
