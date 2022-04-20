using BettingConsoleGame.Domain.Exceptions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities;

public class Wallet
{
    public static Wallet Empty => new(Money.ZeroDollars);
    public static Wallet NonEmpty(Money balance) => new(balance);

    private Wallet(Money balance)
    {
        this.Balance = balance;
        this.Won = Money.ZeroDollars;
    }

    public Money Balance { get; private set; }
    public Money Won { get; private set; }

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

        this.Won -= betAmount;
        this.Balance -= betAmount;
    }

    public void AcceptWin(Money betAmount)
    {
        this.Won += betAmount;
        this.Balance += betAmount;
    }
}
