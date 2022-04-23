using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Entities.Games;
using BettingConsoleGame.Domain.Exceptions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.BetAction;

public class Bet : IAction
{
    private readonly Money bet;
    private readonly IGameFactory gameFactory;

    public Bet(Money betAmount, IGameFactory gameFactory)
    {
        bet = betAmount;
        this.gameFactory = gameFactory;
    }

    public Result<IActionResult> Execute(Wallet wallet)
    {
        try
        {
            var game = gameFactory.CreateBetGame();

            if (!game.IsValidBet(bet))
            {
                return Result<IActionResult>.Fail($"Invalid bet amount: minimum bet is {game.MinimumBet} and maximum {game.MaximumBet}.");
            }

            wallet.Bet(bet);
            var result = game.PlayBets(bet);
            wallet.AcceptWin(result.WinAmount);

            return Result<IActionResult>.Succeed(new BetResult(wallet.Balance, result.WinAmount, result.WinnerType));
        }
        catch (NotEnoughMoneyException notEnoughMoneyException)
        {
            return Result<IActionResult>.Fail(notEnoughMoneyException.Message);
        }
        catch (InvalidBetException invalidBetException)
        {
            return Result<IActionResult>.Fail(invalidBetException.Message);
        }
    }
}
