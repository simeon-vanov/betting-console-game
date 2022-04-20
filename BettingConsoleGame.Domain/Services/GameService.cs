using BettingConsoleGame.Domain.Entities.GameEnvironment.ActionResult;
using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;
using BettingConsoleGame.Domain.Exceptions;
using BettingConsoleGame.Domain.Services;
using BettingConsoleGame.Domain.Services.Randomize;

namespace BettingConsoleGame.Domain.Entities.GameEnvironment;

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
            DepositAction deposit => this.Deposit(deposit, wallet),
            ExitAction exit => new ExitResult(wallet.Won, ResultType.Success),
            WithdrawAction withdraw => this.Withdraw(withdraw, wallet),
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
}

