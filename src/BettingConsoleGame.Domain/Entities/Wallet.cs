﻿using BettingConsoleGame.Domain.Exceptions;
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
        this.Lost = Money.ZeroDollars;
    }

    public Money Balance { get; private set; }

    public Money Won { get; private set; }

    public Money Lost { get; private set; }

    public void Deposit(Money amount)
    {
        this.ValidateAmountIsPositive(amount);

        this.Balance += amount;
    }

    public void Withdraw(Money amount)
    {
        this.ValidateAmountIsPositive(amount);
        
        if (amount > this.Balance)
            throw new NotEnoughMoneyException($"Insufficient funds. {Balance} is not enough to withdraw {amount}.");
        
        this.Balance -= amount;
    }

    public void Bet(Money betAmount)
    {
        this.ValidateAmountIsPositive(betAmount);        
        if (betAmount > this.Balance)
            throw new NotEnoughMoneyException($"Insufficient funds. {Balance} is not enough to bet {betAmount}.");

        this.Lost += betAmount;
        this.Balance -= betAmount;
    }

    public void AcceptWin(Money winAmount)
    {
        this.Won += winAmount;
        this.Balance += winAmount;
    }

    private void ValidateAmountIsPositive(Money amount)
    {
        if (amount <= 0)
        {
            throw new InvalidAmountException($"Amount cannot be negative.");
        }
    }
}
