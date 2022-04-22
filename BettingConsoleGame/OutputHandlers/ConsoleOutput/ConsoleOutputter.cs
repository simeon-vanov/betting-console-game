using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Application.Actions.BetAction;
using BettingConsoleGame.Application.Actions.DepositAction;
using BettingConsoleGame.Application.Actions.ExitAction;
using BettingConsoleGame.Application.Actions.WithdrawAction;
using BettingConsoleGame.Domain.Enums;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.OutputHandlers.ConsoleOutput;

public class ConsoleOutputter : IActionResultOutputter
{
    public void Output(Result<IActionResult> actionResult)
    {
        if (actionResult.ResultType == ResultType.Fail)
        {
            OutputError(actionResult.Errors);
            return;
        }

        if (actionResult.ResultItem.GetType() == typeof(DepositResult))
        {
            OutputDepositResult(actionResult.ResultItem);
        }
        else if (actionResult.ResultItem.GetType() == typeof(ExitResult))
        {
            OutputExitResult(actionResult.ResultItem);
        }
        else if (actionResult.ResultItem.GetType() == typeof(WithdrawResult))
        {
            OutputWithdrawResult(actionResult.ResultItem);
        }
        else if (actionResult.ResultItem.GetType() == typeof(BetResult))
        {
            OutputBetResult(actionResult.ResultItem);
        }

        Console.WriteLine();
    }

    public void OutputError(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void OutputError(IList<string> errors)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        foreach (var error in errors)
            Console.WriteLine(error);

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
    }

    private void OutputExitResult(IActionResult actionResult)
    {
        var exitResult = (ExitResult)actionResult;

        if (exitResult.WonAmount < exitResult.LostAmount)
            Console.WriteLine($"Thanks for playing! You lost {exitResult.LostAmount - exitResult.WonAmount} today :( Better luck next time!");
        else if (exitResult.WonAmount > exitResult.LostAmount)
            Console.WriteLine($"Thanks for playing! Woohooo you won {exitResult.WonAmount - exitResult.LostAmount} today :) Come back soon!");
        else
            Console.WriteLine($"Thanks for playing! Hope to see you again soon!");
    }

    private void OutputDepositResult(IActionResult actionResult)
    {
        var depositResult = (DepositResult)actionResult;

        Console.WriteLine($"Your deposit of {depositResult.Deposited} was successful. Your current balance is: {depositResult.NewBalance}");
    }

    private void OutputWithdrawResult(IActionResult actionResult)
    {
        var depositResult = (WithdrawResult)actionResult;

        Console.WriteLine($"Your withdrawal of {depositResult.Deposited} was successful. Your current balance is: {depositResult.NewBalance}");
    }

    private void OutputBetResult(IActionResult actionResult)
    {
        var bestResult = (BetResult)actionResult;

        if (bestResult.WinAmount > 0)
        {
            Console.WriteLine($"Congrats - you won {bestResult.WinAmount}! Your current balance is: {bestResult.NewBalance}");
        }
        else
        {
            Console.WriteLine($"No luck this time! Your current balance is: {bestResult.NewBalance}");
        }
    }
}
