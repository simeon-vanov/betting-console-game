using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.DepositAction;

public class Deposit : IAction
{
    private readonly Money amount;

    public Deposit(Money amount)
    {
        this.amount = amount;
    }

    public Result<IActionResult> Execute(Wallet wallet)
    {
        wallet.Deposit(amount);

        return Result<IActionResult>.Succesful(new DepositResult(wallet.Balance, amount));
    }
}
