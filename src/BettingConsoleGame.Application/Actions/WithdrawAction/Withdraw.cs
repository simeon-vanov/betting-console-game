using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Exceptions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.WithdrawAction;
public class Withdraw : IAction
{
    private readonly Money amount;

    public Withdraw(Money amount)
    {
        this.amount = amount;
    }

    public Result<IActionResult> Execute(Wallet wallet)
    {
        try
        {
            wallet.Withdraw(amount);
            return Result<IActionResult>.Succeed(new WithdrawResult(wallet.Balance, amount));
        }
        catch (NotEnoughMoneyException notEnoughMoneyException)
        {
            return Result<IActionResult>.Fail(notEnoughMoneyException.Message);
        }
    }
}