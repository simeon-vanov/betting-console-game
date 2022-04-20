using BettingConsoleGame.Domain.Entities.GameEnvironment;
using BettingConsoleGame.Domain.Entities.GameEnvironment.ActionResult;
using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;

namespace BettingConsoleGame;

public class ConsoleActionOutputResult : IActionResultOutputter
{
    public void Output(IActionResult actionResult)
    {
        if(actionResult.GetType() == typeof(DepositResult))
        {
            var depositResult = (DepositResult)actionResult;

            if(depositResult.Type == ResultType.Success)
                Console.WriteLine($"Your deposit of {depositResult.Deposited} was successful. Your current balance is: {depositResult.NewBalance}");
            else
                Console.WriteLine($"Your deposit of {depositResult.Deposited} failed. Your current balance is: {depositResult.NewBalance}");
        }
        else if (actionResult.GetType() == typeof(ExitResult))
        {
            var exitResult = (ExitResult)actionResult;

            if (exitResult.WonAmount < 0)
                Console.WriteLine($"Thanks for playing! You lost {exitResult.WonAmount} today :( Better luck next time!");
            else if (exitResult.WonAmount > 0)
                Console.WriteLine($"Thanks for playing! Woohooo you won {exitResult.WonAmount} today :) Come back soon!");
            else
                Console.WriteLine($"Thanks for playing! Hope to see you again soon!");
        }
    }
}
