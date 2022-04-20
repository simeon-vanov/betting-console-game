using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Parsers;

public class ExitParser : IConsoleActionParser
{
    public IAction Parse(string[] actionParameters)
    {
        if (actionParameters.Length > 1)
        {
            return new InvalidAction();
        }

        return new ExitAction();
    }
}
