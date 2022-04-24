using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Application.Actions.DepositAction;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;

namespace BettingConsoleGame.IntegrationTests.ActionTests;

using static Testing;

public class DepositTests : ActionWithAmountTestBase
{
    protected override string ActionName => "deposit";

    [Test]
    [TestCase(30)]
    [TestCase(15.50)]
    [TestCase(0.01)]
    public void Should_BeAbleToDeposit_WhenValidAmount(decimal amount)
    {
        var wallet = Wallet.Empty;

        DepositAndVerifyBalance(wallet, Money.Dollars(amount), Money.Dollars(amount));
    }

    [Test]
    public void Should_BeAbleToDepositMultipleTimes_WhenValidAmount()
    {
        var wallet = Wallet.Empty;

        DepositAndVerifyBalance(wallet, Money.Dollars(10), Money.Dollars(10));
        DepositAndVerifyBalance(wallet, Money.Dollars(10), Money.Dollars(20));
        DepositAndVerifyBalance(wallet, Money.Dollars(0.01m), Money.Dollars(20.01m));
    }

    private static void DepositAndVerifyBalance(Wallet wallet, Money depositAmount, Money expectedBalance)
    {
        var result = ExecuteAction(wallet, $"deposit {depositAmount.Amount}");

        VerifySuccessResult(result, depositAmount, expectedBalance);
        VerifyEndUserSuccessMessage(depositAmount, expectedBalance);
        VerifyWalletBalance(wallet, expectedBalance);
    }

    private static void VerifySuccessResult(Result<IActionResult> result, Money depositAmount, Money expectedBalance)
    {
        result.Succeeded.Should().BeTrue();

        var withdrawnResult = (DepositResult)result.Value;
        withdrawnResult.Deposited.Should().Be(depositAmount);
        withdrawnResult.NewBalance.Should().Be(expectedBalance);
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
