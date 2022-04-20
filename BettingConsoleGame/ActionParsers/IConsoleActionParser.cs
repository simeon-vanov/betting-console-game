using BettingConsoleGame.Domain.Entities.Action;

namespace BettingConsoleGame.ActionParsers;

public interface IConsoleActionParser
{
    IAction Parse(string[] actionParameters);
}
