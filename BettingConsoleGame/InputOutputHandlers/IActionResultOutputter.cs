using BettingConsoleGame.Domain.Entities.Action;

namespace BettingConsoleGame.InputOutputHandlers;

public interface IActionResultOutputter
{
    void Output(IActionResult actionResult);

    void OutputMessage(string message);
}
