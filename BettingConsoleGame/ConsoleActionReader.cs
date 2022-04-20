using BettingConsoleGame.Domain.Entities.GameEnvironment;
using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;

namespace BettingConsoleGame;

public class ConsoleActionReader : IActionReader
{
    private readonly ConsoleActionParserFactory consoleActionParserFactory;

    public ConsoleActionReader(ConsoleActionParserFactory consoleActionParserFactory)
    {
        this.consoleActionParserFactory = consoleActionParserFactory;
    }

    public IAction GetNextAction()
    {
        Console.WriteLine("Please, Submit Action: ");
        
        var actionString = Console.ReadLine();

        if (string.IsNullOrEmpty(actionString))
        {
            return new InvalidAction();
        }

        var actionParameters = actionString.Split(' ');
        var action = actionParameters[0];

        var actionParser = consoleActionParserFactory.Create(action);

        return actionParser.Parse(actionParameters);
    }
}
