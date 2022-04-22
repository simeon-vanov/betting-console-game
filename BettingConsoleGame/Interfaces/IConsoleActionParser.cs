using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Interfaces;

public interface IConsoleActionParser
{
    Result<IAction> Parse(string[] actionParameters);
}
