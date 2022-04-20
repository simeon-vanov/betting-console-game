using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Entities.Action;

namespace BettingConsoleGame.Domain.Services;

public interface IGameService
{
    IActionResult Execute(IAction action, Wallet wallet);
}
