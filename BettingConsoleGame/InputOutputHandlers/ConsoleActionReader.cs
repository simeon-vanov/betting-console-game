using BettingConsoleGame.ActionParsers;
using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.Exceptions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.InputOutputHandlers;

public class ConsoleActionReader : IActionReader
{
    public Result<IAction> GetNextAction()
    {
        Console.WriteLine("Please, Submit Action: ");

        var actionString = Console.ReadLine();

        if (string.IsNullOrEmpty(actionString))
        {
            throw new UnknownActionException(actionString);
        }

        var actionParameters = actionString.Split(' ');
        var action = actionParameters[0];

        var actionParser = this.CreateParser(action);

        return actionParser.Parse(actionParameters);
    }

    private IConsoleActionParser CreateParser(string action)
    {
        switch (action)
        {
            case "deposit":
                return new DepositParser();
            case "withdraw":
                return new WithdrawParser();
            case "exit":
                return new ExitParser();
            case "bet":
                return new BetParser();
            default:
                throw new UnknownActionException(action);
        }
    }
}
