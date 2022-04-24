using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Application.Actions.BetAction;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Entities.Games.PlayBets;
using BettingConsoleGame.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;

namespace BettingConsoleGame.IntegrationTests.ActionTests;

using static Testing;

public class BetTests : ActionWithAmountTestBase
{
    protected override string ActionName => "bet";

    [Test]
    [TestCase(0.1)]
    [TestCase(9.9)]
    [TestCase(5)]
    public void Should_HaveBigWinUpTo10TimesTheBet_When_ChanceIsBelow10Percent(double randomChance)
    {
        //Arrange
        var initialWalletDollars = Money.Dollars(10);
        var wallet = Wallet.NonEmpty(initialWalletDollars);

        NumberRandomizerServiceMock.SetupChanceWinner(randomChance);
        NumberRandomizerServiceMock.SetupBigWinnerMultiplier(8);
       
        //Act
        var betAmount = Money.Dollars(5);
        var result = ExecuteAction(wallet, $"bet {betAmount.Amount}");

        //Assert
        var expectedWinnerAmount = Money.Dollars(40);
        var expectedBalance = Money.Dollars(45);
        VerifyResult(expectedWinnerAmount, BetGameWinnerType.BigWin, expectedBalance, result);

        VerifyWalletBalance(wallet, expectedWinnerAmount, betAmount, expectedBalance);
    }

    [Test]
    [TestCase(10)]
    [TestCase(49.9)]
    [TestCase(25)]
    public void Should_WinUpTo2TimesTheBet_When_ChanceAbove10PercentAndBelow50Percent(double randomChance)
    {
        // Arrange
        var initialWalletDollars = Money.Dollars(10);
        var wallet = Wallet.NonEmpty(initialWalletDollars);

        NumberRandomizerServiceMock.SetupChanceWinner(randomChance);
        NumberRandomizerServiceMock.SetupWinnerMultiplier(1.5);

        // Arrange
        var betAmount = Money.Dollars(5);
        var result = ExecuteAction(wallet, $"bet {betAmount.Amount}");

        // Assert
        var expectedWinnerAmount = Money.Dollars(7.5m);
        var expectedBalance = Money.Dollars(12.5m);
        VerifyResult(expectedWinnerAmount, BetGameWinnerType.Win, expectedBalance, result);
        VerifyWalletBalance(wallet, expectedWinnerAmount, betAmount, expectedBalance);
    }

    [Test]
    [TestCase(50.1)]
    [TestCase(99.9)]
    [TestCase(75)]
    public void Should_Lose_WhenChanceIsAbove50Percent(double randomChance)
    {
        //Arrange
        var wallet = Wallet.NonEmpty(Money.Dollars(10));

        NumberRandomizerServiceMock.SetupChanceWinner(randomChance);
        
        //Act
        var betAmount = Money.Dollars(5);
        var result = ExecuteAction(wallet, $"bet {betAmount.Amount}");

        //Assert
        var expectedWinnerAmount = Money.ZeroDollars;
        var expectedBalance = Money.Dollars(5);
        VerifyResult(expectedWinnerAmount, BetGameWinnerType.Loss, expectedBalance, result);
        VerifyWalletBalance(wallet, expectedWinnerAmount, betAmount, expectedBalance);
    }

    [Test]
    public void Should_Win60DollarsAndLose35Dollars_When_Bet5AndWin10_Then_Bet10_AndWin20_Then_Bet10AndLose_Then_Bet10AndWin30()
    {
        // Arrange
        var wallet = Wallet.NonEmpty(Money.Dollars(10));

        NumberRandomizerServiceMock.SetupChanceWinner(20);
        NumberRandomizerServiceMock.SetupWinnerMultiplier(2);

        // Act
        ExecuteAction(wallet, $"bet 5");
        ExecuteAction(wallet, $"bet 10");

        NumberRandomizerServiceMock.SetupChanceWinner(70);
        ExecuteAction(wallet, $"bet 10");

        NumberRandomizerServiceMock.SetupChanceWinner(5);
        NumberRandomizerServiceMock.SetupBigWinnerMultiplier(3);
        ExecuteAction(wallet, $"bet 10");


        // Assert
        var expectedWin = Money.Dollars(60);
        var expectedLoss = Money.Dollars(35);

        VerifyWalletBalance(wallet, expectedWin, expectedLoss, Money.Dollars(35));
    }

    [Test]
    public void Should_NotAllowBet_When_InsufficientFunds()
    {
        //Arrange
        var wallet = Wallet.NonEmpty(Money.Dollars(5));

        //Act
        var betAmount = Money.Dollars(10);
        var result = ExecuteAction(wallet, $"bet {betAmount}");

        //Assert
        VerifyFailedResult(result);
        var expectedBalance = Money.Dollars(5);
        VerifyEndUserFailedMessage($"Insufficient funds. {expectedBalance} is not enough to bet {betAmount}.");
        VerifyWalletBalance(wallet, Money.ZeroDollars, Money.ZeroDollars, expectedBalance);
    }

    [Test]
    [TestCase(0.99)]
    [TestCase(10.01)]
    [TestCase(20)]
    public void Should_NotAllowBet_When_InvalidBetAbove10DollarsOrBelow1Dollar(decimal betAmount)
    {
        //Arrange
        var initialAmount = Money.Dollars(15);
        var wallet = Wallet.NonEmpty(initialAmount);

        //Act
        var result = ExecuteAction(wallet, $"bet {betAmount}");

        //Assert
        VerifyFailedResult(result);
        VerifyEndUserFailedMessage($"Invalid bet amount: minimum bet is $1 and maximum $10.");
        VerifyWalletBalance(wallet, Money.ZeroDollars, Money.ZeroDollars, initialAmount);
    }

    private static void VerifyResult(Money winAmount, BetGameWinnerType betGameWinnerType, Money expectedBalance, Result<IActionResult> result)
    {
        VerifySuccessResult(result, winAmount, betGameWinnerType, expectedBalance);
        if (betGameWinnerType == BetGameWinnerType.Loss)
            VerifyEndUserLoserMessage(expectedBalance);
        else
            VerifyEndUserWinnerMessage(winAmount, expectedBalance);
    }

    private static void VerifySuccessResult(
        Result<IActionResult> result, 
        Money winAmount, 
        BetGameWinnerType betGameWinnerType, 
        Money expectedBalance)
    {
        result.Succeeded.Should().BeTrue();

        var withdrawnResult = (BetResult)result.Value;
        withdrawnResult.WinAmount.Should().Be(winAmount);
        withdrawnResult.WinnerType.Should().Be(betGameWinnerType);
        withdrawnResult.NewBalance.Should().Be(expectedBalance);
    }

    private static void VerifyEndUserWinnerMessage(Money winAmount, Money expectedBalance)
    {
        WriteLineMock.Verify(x => x.WriteLine($"Congrats - you won {winAmount}! Your current balance is: {expectedBalance}", ConsoleColor.White), Times.Once);
    }

    private static void VerifyEndUserLoserMessage(Money expectedBalance)
    {
        WriteLineMock.Verify(x => x.WriteLine($"No luck this time! Your current balance is: {expectedBalance}", ConsoleColor.White), Times.Once);
    }
}
