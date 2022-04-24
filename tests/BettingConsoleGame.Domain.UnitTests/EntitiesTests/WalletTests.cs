using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Exceptions;
using BettingConsoleGame.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace BettingConsoleGame.Domain.UnitTests.EntitiesTests;

public class WalletTests
{
    private Wallet sut;

    [SetUp]
    public void SetUp()
    {
        this.sut = Wallet.Empty;
    }
    
    [Test]
    public void Deposit_ShouldIncreaseBalance_When_PositiveAmount()
    {
        sut.Deposit(Money.Dollars(15));
        sut.Deposit(Money.Dollars(2.5m));
        sut.Deposit(Money.Dollars(0.01m));

        sut.Balance.Should().Be(Money.Dollars(17.51m));
    }

    [Test]
    public void Deposit_ShouldNotIncreaseWin_When_PositiveAmount()
    {
        sut.Deposit(Money.Dollars(15));

        sut.Won.Should().Be(Money.ZeroDollars);
    }

    [Test]
    public void Deposit_ShouldThrowException_When_NegativeAmount()
    {
        Assert.Throws<InvalidAmountException>(() => sut.Deposit(Money.Dollars(-1)));
    }

    [Test]
    public void Withdraw_ShouldDecreaseBalance_When_PositiveAmount()
    {
        sut.Deposit(Money.Dollars(15));
        sut.Withdraw(Money.Dollars(15));
        
        sut.Balance.Should().Be(Money.ZeroDollars);
    }

    [Test]
    public void Withdraw_ShouldNotIncreaseLost_When_PositiveAmount()
    {
        sut.Deposit(Money.Dollars(15));
        sut.Withdraw(Money.Dollars(15));

        sut.Lost.Should().Be(Money.ZeroDollars);
    }

    [Test]
    public void Withdraw_ShouldThrowException_When_NegativeAmount()
    {
        Assert.Throws<InvalidAmountException>(() => sut.Withdraw(Money.Dollars(-1)));
    }

    [Test]
    public void Withdraw_ShouldThrowException_When_InsufficientFunds()
    {
        sut.Deposit(Money.Dollars(10));
        Assert.Throws<NotEnoughMoneyException>(() => sut.Withdraw(Money.Dollars(15)));
    }

    [Test]
    public void Bet_ShouldDecreaseBalance_When_PositiveAmount()
    {
        sut.Deposit(Money.Dollars(15));
        sut.Bet(Money.Dollars(10));

        sut.Balance.Should().Be(Money.Dollars(5));
    }

    [Test]
    public void Bet_ShouldIncreaseLost_When_PositiveAmount()
    {
        sut.Deposit(Money.Dollars(15));
        sut.Bet(Money.Dollars(10));

        sut.Lost.Should().Be(Money.Dollars(10));
    }

    [Test]
    public void Bet_ShouldThrowException_When_NegativeAmount()
    {
        Assert.Throws<InvalidAmountException>(() => sut.Bet(Money.Dollars(-1)));
    }

    [Test]
    public void Bet_ShouldThrowException_When_InsufficientFunds()
    {
        sut.Deposit(Money.Dollars(5));
        Assert.Throws<NotEnoughMoneyException>(() => sut.Bet(Money.Dollars(10)));
    }

    [Test]
    public void AcceptWin_ShouldIncreaseBalance_When_PositiveAmount()
    {
        sut.AcceptWin(Money.Dollars(15));
        sut.AcceptWin(Money.Dollars(2.5m));
        sut.AcceptWin(Money.Dollars(0.01m));

        sut.Balance.Should().Be(Money.Dollars(17.51m));
    }

    [Test]
    public void AcceptWin_ShouldIncreaseWin_When_PositiveAmount()
    {
        sut.AcceptWin(Money.Dollars(15));
        sut.AcceptWin(Money.Dollars(10));
        
        sut.Won.Should().Be(Money.Dollars(25));
    }    
}
