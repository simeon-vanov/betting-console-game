using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.InputOutputHandlers;

public interface IActionReader
{
    Result<IAction> GetNextAction();
}

