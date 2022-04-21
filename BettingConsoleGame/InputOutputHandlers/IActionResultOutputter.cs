using BettingConsoleGame.Application.Action.Interfaces;

namespace BettingConsoleGame.InputOutputHandlers;

public interface IActionResultOutputter
{
    void Output(IActionResult actionResult);

    void OutputMessage(string message);
}
