using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions;

public interface IAction
{
    Result<IActionResult> Execute(Wallet wallet);
}
