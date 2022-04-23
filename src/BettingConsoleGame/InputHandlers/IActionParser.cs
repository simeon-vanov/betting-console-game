using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.InputHandlers;

public interface IActionParser
{
    Result<IAction> Parse(string[] actionParameters);
}
