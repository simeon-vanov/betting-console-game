using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.ExitAction;

public class Exit : IAction
{
    public Result<IActionResult> Execute(Wallet wallet)
    {
        return Result<IActionResult>.Succesful(new ExitResult(wallet.Won, wallet.Lost));
    }
}
