using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;

namespace BettingConsoleGame.IntegrationTests.ActionTests;

using static Testing;

public abstract class ActionWithAmountTestBase : TestBase
{
    protected abstract string ActionName { get; }

    [Test]
    public void FailWhenMoreThanOneParameter()
    {
        var initialAmount = Money.Dollars(50);
        var wallet = Wallet.NonEmpty(initialAmount);

        var result = ExecuteAction(wallet, $"{ActionName} 10 invalid");

        VerifyFailedResult(result);
        VerifyEndUserFailedMessage($"Action accepts only amount as parameter.");
        VerifyWalletBalance(wallet, Money.ZeroDollars, Money.ZeroDollars, initialAmount);
    }

    [Test]
    public void FailWhenInvalidAmount()
    {
        var initialAmount = Money.Dollars(50);
        var wallet = Wallet.NonEmpty(initialAmount);

        var result = ExecuteAction(wallet, $"{ActionName} 10.5432");

        VerifyFailedResult(result);
        VerifyEndUserFailedMessage($"Amount must be in the format 0.00.");
        VerifyWalletBalance(wallet, Money.ZeroDollars, Money.ZeroDollars, initialAmount);
    }

    [Test]
    public void FailWhenNegativeAmount()
    {
        var initialAmount = Money.Dollars(50);
        var wallet = Wallet.NonEmpty(initialAmount);

        var result = ExecuteAction(wallet, $"{ActionName} -10");

        VerifyFailedResult(result);
        VerifyEndUserFailedMessage($"Amount must be positive number bigger than 0.");
        VerifyWalletBalance(wallet, Money.ZeroDollars, Money.ZeroDollars, initialAmount);
    }

    [Test]
    public void FailWhenZeroAmount()
    {
        var initialAmount = Money.Dollars(50);
        var wallet = Wallet.NonEmpty(initialAmount);

        var result = ExecuteAction(wallet, $"{ActionName} 0");

        VerifyFailedResult(result);
        VerifyEndUserFailedMessage($"Amount must be positive number bigger than 0.");
        VerifyWalletBalance(wallet, Money.ZeroDollars, Money.ZeroDollars, initialAmount);
    }

    protected static void VerifyFailedResult(Result<IActionResult> result)
    {
        result.Failed.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().HaveCountGreaterThan(0);
    }

    protected static void VerifyEndUserFailedMessage(string message)
    {
        WriteLineMock.Verify(x => x.WriteLine(message, ConsoleColor.Red), Times.Once);
    }

    protected static void VerifyWalletBalance(Wallet wallet, Money won, Money lost, Money expectedBalance)
    {
        wallet.Balance.Should().Be(expectedBalance);
        wallet.Lost.Should().Be(lost);
        wallet.Won.Should().Be(won);
    }
}
