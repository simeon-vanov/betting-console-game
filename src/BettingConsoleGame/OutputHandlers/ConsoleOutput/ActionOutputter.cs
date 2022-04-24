using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Application.Actions.BetAction;
using BettingConsoleGame.Application.Actions.DepositAction;
using BettingConsoleGame.Application.Actions.ExitAction;
using BettingConsoleGame.Application.Actions.WithdrawAction;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.OutputHandlers.ConsoleOutput;

public class ActionOutputter : IActionResultOutputter
{
    private readonly IOutputHandler outputHandler;

    public ActionOutputter(IOutputHandler outputHandler)
    {
        this.outputHandler = outputHandler;
    }

    public void Output(Result<IActionResult> actionResult)
    {
        if (actionResult.Failed)
        {
            OutputError(actionResult.Errors);
            return;
        }

        if (actionResult.Value.GetType() == typeof(DepositResult))
        {
            OutputDepositResult(actionResult.Value);
        }
        else if (actionResult.Value.GetType() == typeof(ExitResult))
        {
            OutputExitResult(actionResult.Value);
        }
        else if (actionResult.Value.GetType() == typeof(WithdrawResult))
        {
            OutputWithdrawResult(actionResult.Value);
        }
        else if (actionResult.Value.GetType() == typeof(BetResult))
        {
            OutputBetResult(actionResult.Value);
        }

        outputHandler.WriteLine();
    }

    public void OutputError(string error)
    {
        outputHandler.WriteLine(error, ConsoleColor.Red);
        outputHandler.WriteLine();
    }

    public void OutputError(IList<string> errors)
    {
        foreach (var error in errors)
            outputHandler.WriteLine(error, ConsoleColor.Red);

        outputHandler.WriteLine();
    }

    private void OutputExitResult(IActionResult actionResult)
    {
        var exitResult = (ExitResult)actionResult;

        if (exitResult.WonAmount < exitResult.LostAmount)
            outputHandler.WriteLine($"Thanks for playing! You lost {exitResult.LostAmount - exitResult.WonAmount} today :( Better luck next time!");
        else if (exitResult.WonAmount > exitResult.LostAmount)
            outputHandler.WriteLine($"Thanks for playing! You won {exitResult.WonAmount - exitResult.LostAmount} today :) Come back soon!");
        else
            outputHandler.WriteLine($"Thanks for playing! Hope to see you again soon!");
    }

    private void OutputDepositResult(IActionResult actionResult)
    {
        var depositResult = (DepositResult)actionResult;

        outputHandler.WriteLine($"Your deposit of {depositResult.Deposited} was successful. Your current balance is: {depositResult.NewBalance}");
    }

    private void OutputWithdrawResult(IActionResult actionResult)
    {
        var depositResult = (WithdrawResult)actionResult;

        outputHandler.WriteLine($"Your withdrawal of {depositResult.Withdrawn} was successful. Your current balance is: {depositResult.NewBalance}");
    }

    private void OutputBetResult(IActionResult actionResult)
    {
        var bestResult = (BetResult)actionResult;

        if (bestResult.WinAmount > 0)
        {
            outputHandler.WriteLine($"Congrats - you won {bestResult.WinAmount}! Your current balance is: {bestResult.NewBalance}");
        }
        else
        {
            outputHandler.WriteLine($"No luck this time! Your current balance is: {bestResult.NewBalance}");
        }
    }
}
