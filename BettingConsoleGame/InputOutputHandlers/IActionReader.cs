using BettingConsoleGame.Domain.Entities.Action;

namespace BettingConsoleGame.InputOutputHandlers;

public interface IActionReader
{
    IAction GetNextAction();
}

