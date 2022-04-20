using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;

namespace BettingConsoleGame.InputOutputHandlers;

public interface IActionReader
{
    IAction GetNextAction();
}

