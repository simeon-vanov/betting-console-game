using BettingConsoleGame.Application.Actions.Interfaces;

namespace BettingConsoleGame.InputOutputHandlers;

public interface IActionResultOutputter
{
    void Output(IActionResult actionResult);

    void OutputMessage(string message);

    void OutputErrors(IList<string> errors);

}
