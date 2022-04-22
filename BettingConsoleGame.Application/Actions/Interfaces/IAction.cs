using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.Interfaces;

public interface IAction
{
    Result<IActionResult> Execute(Wallet wallet);
}
