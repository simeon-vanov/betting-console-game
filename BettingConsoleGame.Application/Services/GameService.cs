using BettingConsoleGame.Application.Actions.ActionResult;
using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Application.Actions.Types;
using BettingConsoleGame.Application.Services.Interfaces;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Entities.SlotGame;
using BettingConsoleGame.Domain.Enums;
using BettingConsoleGame.Domain.Exceptions;
using BettingConsoleGame.Domain.Services;

namespace BettingConsoleGame.Application.Services;

public class GameService : IGameService
{
    private readonly INumberRandomizerService numberRandomizerService;

    public GameService(INumberRandomizerService numberRandomizerService)
    {
        this.numberRandomizerService = numberRandomizerService;
    }

    public IActionResult Execute(IAction action, Wallet wallet)
    {
        return action switch
        {
            DepositAction deposit => Deposit(deposit, wallet),
            ExitAction exit => new ExitResult(wallet.Won, ResultType.Success),
            WithdrawAction withdraw => Withdraw(withdraw, wallet),
            BetAction bet => Bet(bet, wallet),

            _ => throw new UnknownActionException(action.GetType().Name)
        };
    }

    private WithdrawResult Withdraw(WithdrawAction withdraw, Wallet wallet)
    {
        wallet.Withdraw(withdraw.Amount);

        return new WithdrawResult(wallet.Balance, withdraw.Amount, ResultType.Success);
    }

    private DepositResult Deposit(DepositAction deposit, Wallet wallet)
    {
        wallet.Deposit(deposit.Amount);

        return new DepositResult(wallet.Balance, deposit.Amount, ResultType.Success);
    }

    private BetResult Bet(BetAction bet, Wallet wallet)
    {
        wallet.Bet(bet.Amount);

        var game = new BetGame(numberRandomizerService);
        var result = game.PlayBets(bet.Amount);

        wallet.AcceptWin(result.WinAmount);

        return new BetResult(wallet.Balance, result.WinAmount, result.WinnerType, ResultType.Success);
    }
}

