using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.InputOutputHandlers;

public interface IActionResultOutputter
{
    void Output(Result<IActionResult> actionResult);

    void OutputError(string error);

    void OutputError(IList<string> errors);

}
