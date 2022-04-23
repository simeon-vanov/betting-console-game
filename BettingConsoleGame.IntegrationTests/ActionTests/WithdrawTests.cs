using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Application.Actions.WithdrawAction;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.ValueObjects;
using BettingConsoleGame.Game;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using static BettingConsoleGame.IntegrationTests.Testing;

namespace BettingConsoleGame.IntegrationTests.ActionTests;

public class WithdrawTests : TestBase
{
    [Test]
    [TestCase(30, 30, 0)]
    [TestCase(30, 15.50, 14.50)]
    [TestCase(21.14, 21.13, 0.01)]
    public void WalletShouldBeAbleToWithdrawWhenBalanceIsSufficient(decimal walletAmount, decimal withdraw, decimal newBalance)
    {
        var initialWalletDollars = Money.Dollars(walletAmount);
        var wallet = Wallet.NonEmpty(initialWalletDollars);

        WithdrawAndVerifyBalance(wallet, Money.Dollars(withdraw), Money.Dollars(newBalance));
    }

    [Test]
    public void ShouldAllowMultipleWithdrawWhenSufficientAmount()
    {
        var initialAmount = Money.Dollars(50);
        var wallet = Wallet.NonEmpty(initialAmount);

        WithdrawAndVerifyBalance(wallet, Money.Dollars(10), Money.Dollars(40));
        WithdrawAndVerifyBalance(wallet, Money.Dollars(15), Money.Dollars(25));
        WithdrawInsufficientFundsAndVerifyBalance(Money.Dollars(50.50m), wallet, Money.Dollars(25));
        WithdrawAndVerifyBalance(wallet, Money.Dollars(25), Money.ZeroDollars);
    }

    [Test]
    public void ShouldNotAllowWithdrawWhenInsufficientFunds()
    {
        var initialAmount = Money.Dollars(50);
        var wallet = Wallet.NonEmpty(initialAmount);

        WithdrawInsufficientFundsAndVerifyBalance(Money.Dollars(50.50m), wallet, initialAmount);
    }

    [Test]
    public void FailWhenMoreThanOneParameter()
    {
        var initialAmount = Money.Dollars(50);
        var wallet = Wallet.NonEmpty(initialAmount);

        var result = ExecuteAction(wallet, "withdraw 10 invalid");

        VerifyFailedResult(result);
        VerifyEndUserFailedMessage($"Action accepts only amount as parameter.");
        VerifyWalletBalance(wallet, initialAmount);
    }

    [Test]
    public void FailWhenInvalidAmount()
    {
        var initialAmount = Money.Dollars(50);
        var wallet = Wallet.NonEmpty(initialAmount);

        var result = ExecuteAction(wallet, "withdraw 10.5432");

        VerifyFailedResult(result);
        VerifyEndUserFailedMessage($"Amount must be in the format 0.00.");
        VerifyWalletBalance(wallet, initialAmount);
    }

    [Test]
    public void FailWhenNegativeAmount()
    {
        var initialAmount = Money.Dollars(50);
        var wallet = Wallet.NonEmpty(initialAmount);

        var result = ExecuteAction(wallet, "withdraw -10");

        VerifyFailedResult(result);
        VerifyEndUserFailedMessage($"Amount must be positive number bigger than 0.");
        VerifyWalletBalance(wallet, initialAmount);
    }

    [Test]
    public void FailWhenZeroAmount()
    {
        var initialAmount = Money.Dollars(50);
        var wallet = Wallet.NonEmpty(initialAmount);

        var result = ExecuteAction(wallet, "withdraw 0");

        VerifyFailedResult(result);
        VerifyEndUserFailedMessage($"Amount must be positive number bigger than 0.");
        VerifyWalletBalance(wallet, initialAmount);
    }

    private static void WithdrawInsufficientFundsAndVerifyBalance(Money withdrawAmount, Wallet wallet, Money expectedBalance)
    {
        var result = ExecuteAction(wallet, "withdraw 50.50");

        VerifyFailedResult(result);
        VerifyEndUserFailedMessage($"Insufficient funds. {expectedBalance} is not enough to withdraw {withdrawAmount}.");
        VerifyWalletBalance(wallet, expectedBalance);
    }

    private static void WithdrawAndVerifyBalance(Wallet wallet, Money withdrawAmount, Money expectedBalance)
    {
        var result = ExecuteAction(wallet, $"withdraw {withdrawAmount.Amount}");

        VerifySuccessResult(result, withdrawAmount, expectedBalance);
        VerifyEndUserSuccessMessage(withdrawAmount, expectedBalance);
        VerifyWalletBalance(wallet, expectedBalance);
    }

    private static void VerifyFailedResult(Result<IActionResult> result)
    {
        result.Failed.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().HaveCountGreaterThan(0);
    }

    private static void VerifySuccessResult(Result<IActionResult> result, Money withdrawnAmount, Money expectedBalance)
    {
        result.Succeeded.Should().BeTrue();

        var withdrawnResult = (WithdrawResult)result.Value;
        withdrawnResult.Withdrawn.Should().Be(withdrawnAmount);

        withdrawnResult.NewBalance.Should().Be(expectedBalance);
    }

    private static void VerifyEndUserFailedMessage(string message)
    {
        WriteLineMock.Verify(x => x.WriteLine(message, ConsoleColor.Red), Times.Once);
    }

    private static void VerifyEndUserSuccessMessage(Money withdrawnAmount, Money expectedBalance)
    {
        WriteLineMock.Verify(x => x.WriteLine($"Your withdrawal of {withdrawnAmount} was successful. Your current balance is: {expectedBalance}", ConsoleColor.White), Times.Once);
    }

    private static void VerifyWalletBalance(Wallet wallet, Money expectedBalance)
    {
        wallet.Balance.Should().Be(expectedBalance);
        wallet.Lost.Should().Be(Money.ZeroDollars);
        wallet.Won.Should().Be(Money.ZeroDollars);
    }
}
