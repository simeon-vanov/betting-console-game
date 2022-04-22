using BettingConsoleGame.Application.Actions.ActionResult;
using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.Types;

public class Exit : IAction
{
    public Result<IActionResult> Execute(Wallet wallet)
    {
        return Result<IActionResult>.Succesful(new ExitResult(wallet.Won, wallet.Lost));
    }
}
