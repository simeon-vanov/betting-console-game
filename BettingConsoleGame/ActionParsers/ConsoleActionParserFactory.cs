using BettingConsoleGame.Domain.Exceptions;
using BettingConsoleGame.Parsers;

namespace BettingConsoleGame;

public class ConsoleActionParserFactory
{
    public IConsoleActionParser Create(string action)
    {
        switch (action)
        {
            case "deposit":
                return new DepositParser();
            case "exit":
                return new ExitParser();
            default:
                throw new UnknownActionException(action);
        }
    }
}
