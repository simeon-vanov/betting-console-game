using BettingConsoleGame.Domain.Entities.GameEnvironment.ActionResult;
using BettingConsoleGame.Domain.Entities.GameEnvironment.ActionResult.ActionResult;
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
            _ => throw new NotImplementedException()
        };
    }

    private DepositResult Deposit(DepositAction deposit, Wallet wallet)
    {
        wallet.Deposit(deposit.Amount);

        return new DepositResult(wallet.Balance, deposit.Amount, ResultType.Success);
    }
}

