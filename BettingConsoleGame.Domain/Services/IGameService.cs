using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;

namespace BettingConsoleGame.Domain.Services;

public interface IGameService
{
    IActionResult Execute(IAction action, Wallet wallet);
}
