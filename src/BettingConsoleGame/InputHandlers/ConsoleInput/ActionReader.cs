using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Domain.Exceptions;
using BettingConsoleGame.Domain.ValueObjects;
using BettingConsoleGame.InputHandlers.ActionParsers;
using BettingConsoleGame.OutputHandlers;

namespace BettingConsoleGame.InputHandlers.ConsoleInput;

public class ActionReader : IActionReader
{
    private readonly IActionFactory actionFactory;
    private readonly IInputHandler inputHandler;
    private readonly IOutputHandler outputHandler;

    public ActionReader(IActionFactory actionFactory, IInputHandler inputHandler, IOutputHandler outputHandler)
    {
        this.actionFactory = actionFactory;
        this.inputHandler = inputHandler;
        this.outputHandler = outputHandler;
    }

    public Result<IAction> GetNextAction()
    {
        outputHandler.WriteLine("Please, Submit Action: ");

        var actionString = inputHandler.ReadLine();

        if (string.IsNullOrEmpty(actionString))
        {
            throw new UnknownActionException(actionString);
        }

        var actionParameters = actionString.Split(' ');
        var action = actionParameters[0];

        try
        {
            var actionParser = CreateParser(action);

            return actionParser.Parse(actionParameters);
        }
        catch (UnknownActionException unknownActionException)
        {
            return Result<IAction>.Fail($"{unknownActionException.Message}. Known actions are: deposit, withdraw, bet and exit");
        }
    }

    private IActionParser CreateParser(string action)
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
