using BettingConsoleGame.Domain.Entities.Games;

namespace BettingConsoleGame.Domain.Factories.Interfaces;

public interface IGameFactory
{
    public BetGame CreateBetGame();
}
