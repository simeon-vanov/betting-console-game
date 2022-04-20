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
    }
}
