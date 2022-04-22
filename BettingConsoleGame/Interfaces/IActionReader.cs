using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Interfaces;

public interface IActionReader
{
    Result<IAction> GetNextAction();
}

