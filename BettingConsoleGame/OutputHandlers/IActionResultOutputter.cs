using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.OutputHandlers;

public interface IActionResultOutputter
{
    void Output(Result<IActionResult> actionResult);

    void OutputError(string error);

    void OutputError(IList<string> errors);

}
