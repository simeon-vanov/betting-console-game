using BettingConsoleGame.Domain.Entities.Games.PlayBets;
using BettingConsoleGame.Domain.Services;

namespace BettingConsoleGame.Domain.Entities.Games.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly INumberRandomizerService numberRandomizerService;

        public GameFactory(INumberRandomizerService numberRandomizerService)
        {
            this.numberRandomizerService = numberRandomizerService;
        }

        public BetGame CreateBetGame()
        {
            return new BetGame(numberRandomizerService);
        }
    }
}
