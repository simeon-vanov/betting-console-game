using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Application.Actions.DepositAction;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.ValueObjects;
using BettingConsoleGame.Game;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using static BettingConsoleGame.IntegrationTests.Testing;

namespace BettingConsoleGame.IntegrationTests.ActionTests;

public class DepositTests : TestBase
{
    [Test]
    [TestCase(30)]
    [TestCase(15.50)]
    [TestCase(0.01)]
    public void WalletShouldBeAbleToDeposit(decimal amount)
    {
        var wallet = Wallet.Empty;

        DepositAndVerifyBalance(wallet, Money.Dollars(amount), Money.Dollars(amount));
    }

    [Test]
    public void WalletShouldBeAbleToDepositMultipleTimes()
    {
        var wallet = Wallet.Empty;

        DepositAndVerifyBalance(wallet, Money.Dollars(10), Money.Dollars(10));
        DepositAndVerifyBalance(wallet, Money.Dollars(10), Money.Dollars(20));
        DepositAndVerifyBalance(wallet, Money.Dollars(0.01m), Money.Dollars(20.01m));
    }

    [Test]
    public void FailWhenMoreThanOneParameter()
    {
        var wallet = Wallet.NonEmpty(Money.ZeroDollars);

        var result = ExecuteAction(wallet, "deposit 10 invalid");

        VerifyFailedResult(result);
        VerifyEndUserFailedMessage($"Action accepts only amount as parameter.");
        VerifyWalletBalance(wallet, Money.ZeroDollars);
    }

    [Test]
    public void FailWhenInvalidAmount()
    {
        var wallet = Wallet.NonEmpty(Money.ZeroDollars);

        var result = ExecuteAction(wallet, "deposit 10.5432");

        VerifyFailedResult(result);
        VerifyEndUserFailedMessage($"Amount must be in the format 0.00.");
        VerifyWalletBalance(wallet, Money.ZeroDollars);
    }

    [Test]
    public void FailWhenNegativeAmount()
    {
        var wallet = Wallet.NonEmpty(Money.ZeroDollars);

        var result = ExecuteAction(wallet, "deposit -10");

        VerifyFailedResult(result);
        VerifyEndUserFailedMessage($"Amount must be positive number bigger than 0.");
        VerifyWalletBalance(wallet, Money.ZeroDollars);
    }

    [Test]
    public void FailWhenZeroAmount()
    {
        var wallet = Wallet.NonEmpty(Money.ZeroDollars);

        var result = ExecuteAction(wallet, "deposit 0");

        VerifyFailedResult(result);
        VerifyEndUserFailedMessage($"Amount must be positive number bigger than 0.");
        VerifyWalletBalance(wallet, Money.ZeroDollars);
    }


    private static void DepositAndVerifyBalance(Wallet wallet, Money depositAmount, Money expectedBalance)
    {
        var result = ExecuteAction(wallet, $"deposit {depositAmount.Amount}");

        VerifySuccessResult(result, depositAmount, expectedBalance);
        VerifyEndUserSuccessMessage(depositAmount, expectedBalance);
        VerifyWalletBalance(wallet, expectedBalance);
    }

    private static void VerifyFailedResult(Result<IActionResult> result)
    {
        result.Failed.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().HaveCountGreaterThan(0);
    }

    private static void VerifySuccessResult(Result<IActionResult> result, Money depositAmount, Money expectedBalance)
    {
        result.Succeeded.Should().BeTrue();

        var withdrawnResult = (DepositResult)result.Value;
        withdrawnResult.Deposited.Should().Be(depositAmount);
        withdrawnResult.NewBalance.Should().Be(expectedBalance);
    }

    private static void VerifyEndUserFailedMessage(string message)
    {
        WriteLineMock.Verify(x => x.WriteLine(message, ConsoleColor.Red), Times.Once);
    }

    private static void VerifyEndUserSuccessMessage(Money depositAmount, Money expectedBalance)
    {
        WriteLineMock.Verify(x => x.WriteLine($"Your deposit of {depositAmount} was successful. Your current balance is: {expectedBalance}", ConsoleColor.White), Times.Once);
    }

    private static void VerifyWalletBalance(Wallet wallet, Money expectedBalance)
    {
        wallet.Balance.Should().Be(expectedBalance);
        wallet.Lost.Should().Be(Money.ZeroDollars);
        wallet.Won.Should().Be(Money.ZeroDollars);
    }
}
