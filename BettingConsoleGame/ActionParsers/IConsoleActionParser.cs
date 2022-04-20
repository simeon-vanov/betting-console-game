using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;

namespace BettingConsoleGame;

public interface IConsoleActionParser
{
    IAction Parse(string[] actionParameters);
}
