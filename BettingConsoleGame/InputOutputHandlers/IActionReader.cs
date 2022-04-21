using BettingConsoleGame.Application.Action.Interfaces;

namespace BettingConsoleGame.InputOutputHandlers;

public interface IActionReader
{
    IAction GetNextAction();
}

