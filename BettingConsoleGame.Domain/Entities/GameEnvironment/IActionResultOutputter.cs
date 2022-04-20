using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;

namespace BettingConsoleGame.Domain.Entities.GameEnvironment;

public interface IActionResultOutputter
{
    void Output(IActionResult actionResult);
}
