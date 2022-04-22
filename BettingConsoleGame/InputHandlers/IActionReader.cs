using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.InputHandlers;

public interface IActionReader
{
    Result<IAction> GetNextAction();
}

