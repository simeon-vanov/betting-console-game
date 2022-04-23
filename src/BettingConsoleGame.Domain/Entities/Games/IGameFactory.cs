using BettingConsoleGame.Domain.Entities.Games.PlayBets;

namespace BettingConsoleGame.Domain.Entities.Games;

public interface IGameFactory
{
    public BetGame CreateBetGame();
}
