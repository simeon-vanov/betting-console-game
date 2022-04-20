﻿using BettingConsoleGame.Domain.Entities.GameEnvironment.ActionResult;
using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;

namespace BettingConsoleGame.InputOutputHandlers;

public class ConsoleOutputter : IActionResultOutputter
{
    public void Output(IActionResult actionResult)
    {
        if (actionResult.GetType() == typeof(DepositResult))
        {
            OutputDepositResult(actionResult);
        }
        else if (actionResult.GetType() == typeof(ExitResult))
        {
            OutputExitResult(actionResult);
        }
        else if (actionResult.GetType() == typeof(WithdrawResult))
        {
            OutputWithdrawResult(actionResult);
        }

        Console.WriteLine();
    }

    public void OutputMessage(string message)
    {
        Console.WriteLine(message);
    }

    private void OutputExitResult(IActionResult actionResult)
    {
        var exitResult = (ExitResult)actionResult;

        if (exitResult.WonAmount < 0)
            Console.WriteLine($"Thanks for playing! You lost {exitResult.WonAmount} today :( Better luck next time!");
        else if (exitResult.WonAmount > 0)
            Console.WriteLine($"Thanks for playing! Woohooo you won {exitResult.WonAmount} today :) Come back soon!");
        else
            Console.WriteLine($"Thanks for playing! Hope to see you again soon!");
    }

    private void OutputDepositResult(IActionResult actionResult)
    {
        var depositResult = (DepositResult)actionResult;

        if (depositResult.Type == ResultType.Success)
            Console.WriteLine($"Your deposit of {depositResult.Deposited} was successful. Your current balance is: {depositResult.NewBalance}");
        else
            Console.WriteLine($"Your deposit of {depositResult.Deposited} failed. Your current balance is: {depositResult.NewBalance}");
    }

    private void OutputWithdrawResult(IActionResult actionResult)
    {
        var depositResult = (WithdrawResult)actionResult;

        if (depositResult.Type == ResultType.Success)
            Console.WriteLine($"Your withdrawal of {depositResult.Deposited} was successful. Your current balance is: {depositResult.NewBalance}");
        else
            Console.WriteLine($"Your withdrawal of {depositResult.Deposited} failed. Your current balance is: {depositResult.NewBalance}");
    }
}
