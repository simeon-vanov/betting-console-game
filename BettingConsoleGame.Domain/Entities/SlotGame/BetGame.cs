using BettingConsoleGame.Domain.Exceptions;
using BettingConsoleGame.Domain.Services.Randomize;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities.SlotGame;

public class BetGame
{
    private readonly INumberRandomizerService numberRandomizerService;
    private const double ChanceForBigWinner = 10;
    private const double ChanceForWinner = 40;
    
    public BetGame(INumberRandomizerService numberRandomizerService)
    {
        this.numberRandomizerService = numberRandomizerService;
    }

    public BetGameResult PlayBets(Money bet)
    {
        if(bet < Money.OneDollar || bet > Money.TenDollars)
        {
            throw new InvalidBetException(Money.OneDollar, Money.TenDollars);
        }

        var chance = numberRandomizerService.GetRandomDouble(0, 100);

        if(chance < ChanceForBigWinner)
        {
            // 10% of the games result in big win between 2 and 10 times the bet
            var winAmount = bet * numberRandomizerService.GetRandomDouble(2, 10);
            
            return new BetGameResult(winAmount, BetGameWinnerType.BigWin);
        }
        else if(chance < ChanceForWinner + ChanceForBigWinner)
        {
            // 40% of the games result in a win up to 2 times the bet amount
            var winAmount = bet * numberRandomizerService.GetRandomDouble(1, 2);

            return new BetGameResult(winAmount, BetGameWinnerType.Win);
        }

        // 50% of the games result in a loss
        return new BetGameResult(Money.ZeroDollars, BetGameWinnerType.Loss);
    }
}
