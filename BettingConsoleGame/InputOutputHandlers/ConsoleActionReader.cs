using BettingConsoleGame.ActionParsers;
using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.Exceptions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.InputOutputHandlers;

public class ConsoleActionReader : IActionReader
{
    private readonly IActionFactory actionFactory;

    public ConsoleActionReader(IActionFactory actionFactory)
    {
        this.actionFactory = actionFactory;
    }

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

        try
        {
            var actionParser = this.CreateParser(action);

            return actionParser.Parse(actionParameters);
        }
        catch(UnknownActionException unknownActionException)
        {
            return Result<IAction>.Failed($"{unknownActionException.Message}. Known actions are: deposit, withdraw, bet and exit");
        }
    }

    private IConsoleActionParser CreateParser(string action)
    {
        switch (action)
        {
            case "deposit":
                return new DepositParser(actionFactory);
            case "withdraw":
                return new WithdrawParser(actionFactory);
            case "exit":
                return new ExitParser(actionFactory);
            case "bet":
                return new BetParser(actionFactory);
            default:
                throw new UnknownActionException(action);
        }
    }
}
