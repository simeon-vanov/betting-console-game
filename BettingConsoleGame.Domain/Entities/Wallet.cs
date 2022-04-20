using BettingConsoleGame.Domain.Exceptions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities;

public class Wallet
{
    public static Wallet EmptyWallet => new(Money.ZeroDollars);
    public static Wallet NonEmptyWallet(Money balance) => new(balance);

    private Wallet(Money balance)
    {
        this.Balance = balance;
    }

    public Money Balance { get; private set; }

    public void Deposit(Money amount)
    {
        this.Balance += amount;
    }

    public void Withdraw(Money amount)
    {
        if (amount > this.Balance)
            throw new NotEnoughMoneyException();
        
        this.Balance -= amount;
    }

    public void Bet(Money betAmount)
    {
        if (betAmount > this.Balance)
            throw new NotEnoughMoneyException();

        this.Balance -= betAmount;
    }

    public void AcceptWin(Money betAmount)
    {
        this.Balance += betAmount;
    }
}
