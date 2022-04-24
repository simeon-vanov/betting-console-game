using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Game;

public interface IGameActionHandler
{
    Result<IActionResult> Execute(Wallet wallet);
}
