using BettingConsoleGame.Application.Action.Interfaces;

namespace BettingConsoleGame.ActionParsers;

public interface IConsoleActionParser
{
    IAction Parse(string[] actionParameters);
}
