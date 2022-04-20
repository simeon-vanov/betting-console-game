using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;

namespace BettingConsoleGame.InputOutputHandlers;

public interface IActionResultOutputter
{
    void Output(IActionResult actionResult);

    void OutputMessage(string message);
}
