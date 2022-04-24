using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Application.Actions.WithdrawAction;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;

namespace BettingConsoleGame.IntegrationTests.ActionTests;

using static Testing;

public class WithdrawTests : ActionWithAmountTestBase
{
    protected override string ActionName => "withdraw";

    [Test]
    [TestCase(30, 30, 0)]
    [TestCase(30, 15.50, 14.50)]
    [TestCase(21.14, 21.13, 0.01)]
    public void Should_BeAbleToWithdraw_WhenAmountIsSufficient(decimal walletAmount, decimal withdraw, decimal newBalance)
    {
        var initialWalletDollars = Money.Dollars(walletAmount);
        var wallet = Wallet.NonEmpty(initialWalletDollars);

        WithdrawAndVerifyBalance(wallet, Money.Dollars(withdraw), Money.Dollars(newBalance));
    }

    [Test]
    public void Should_BeAbleToWithdrawMultipleTimes_WhenAmountIsSufficient()
    {
        var initialAmount = Money.Dollars(50);
        var wallet = Wallet.NonEmpty(initialAmount);

        WithdrawAndVerifyBalance(wallet, Money.Dollars(10), Money.Dollars(40));
        WithdrawAndVerifyBalance(wallet, Money.Dollars(15), Money.Dollars(25));
        WithdrawInsufficientFundsAndVerifyBalance(Money.Dollars(50.50m), wallet, Money.Dollars(25));
        WithdrawAndVerifyBalance(wallet, Money.Dollars(25), Money.ZeroDollars);
    }

    [Test]
    public void Should_NotAllowWithdraw_When_InsufficientFunds()
    {
        var initialAmount = Money.Dollars(50);
        var wallet = Wallet.NonEmpty(initialAmount);

        WithdrawInsufficientFundsAndVerifyBalance(Money.Dollars(50.50m), wallet, initialAmount);
    }

    private static void WithdrawInsufficientFundsAndVerifyBalance(Money withdrawAmount, Wallet wallet, Money expectedBalance)
    {
        var result = ExecuteAction(wallet, $"withdraw {withdrawAmount}");

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

    private static void VerifySuccessResult(Result<IActionResult> result, Money withdrawnAmount, Money expectedBalance)
    {
        result.Succeeded.Should().BeTrue();

        var withdrawnResult = (WithdrawResult)result.Value;
        withdrawnResult.Withdrawn.Should().Be(withdrawnAmount);

        withdrawnResult.NewBalance.Should().Be(expectedBalance);
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
