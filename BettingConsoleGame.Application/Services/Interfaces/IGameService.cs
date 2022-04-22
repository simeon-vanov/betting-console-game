using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.Entities;

namespace BettingConsoleGame.Application.Services.Interfaces;

public interface IGameService
{
    IActionResult Execute(IAction action, Wallet wallet);
}
