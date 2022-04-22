using BettingConsoleGame.Domain.Entities.Games;
using BettingConsoleGame.Domain.Factories.Interfaces;
using BettingConsoleGame.Domain.Services;

namespace BettingConsoleGame.Domain.Factories
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
