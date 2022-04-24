using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.ValueObjects;
using Moq;
using NUnit.Framework;
using System;

namespace BettingConsoleGame.IntegrationTests.ActionTests;

using static Testing;

public class ExitTests : TestBase
{
    [Test]
    public void Should_ShowPositiveMessage_When_PlayerWonMoneyAndExits()
    {
        //Arrange
        var wallet = Wallet.NonEmpty(Money.Dollars(10));
        wallet.Bet(Money.Dollars(5));
        wallet.AcceptWin(Money.Dollars(10));

        //Act
        ExecuteAction(wallet, $"exit");

        //Assert
        VerifyPositiveMessage(Money.Dollars(5));
    }

    [Test]
    public void Should_ShowEncourigingMessage_When_PlayerLostMoneyAndExits()
    {
        //Arrange
        var wallet = Wallet.NonEmpty(Money.Dollars(10));
        wallet.Bet(Money.Dollars(10));
        wallet.AcceptWin(Money.Dollars(0));

        //Act
        ExecuteAction(wallet, $"exit");

        //Assert
        VerifyEncourigingMessage(Money.Dollars(10));
    }

    [Test]
    public void Should_ShowThankYouMessage_When_PlayerDidNotWinOrLoseAndExits()
    {
        //Arrange
        var wallet = Wallet.NonEmpty(Money.Dollars(10));

        //Act
        ExecuteAction(wallet, $"exit");

        //Assert
        VerifyThankYouMessage();
    }

    [Test]
    public void Should_FailToExit_When_MultipleParametersArePassed()
    {
        //Arrange
        var wallet = Wallet.NonEmpty(Money.Dollars(10));

        //Act
        ExecuteAction(wallet, $"exit exit");

        //Assert
        VerifyFailedForMultiplieArgumentMessage();
    }

    private static void VerifyFailedForMultiplieArgumentMessage()
    {
        WriteLineMock.Verify(x => x.WriteLine($"Exit action does not accept any parameters.", ConsoleColor.Red), Times.Once);
    }

    private static void VerifyThankYouMessage()
    {
        WriteLineMock.Verify(x => x.WriteLine($"Thanks for playing! Hope to see you again soon!", ConsoleColor.White), Times.Once);
    }

    private static void VerifyPositiveMessage(Money wonAmount)
    {
        WriteLineMock.Verify(x => x.WriteLine($"Thanks for playing! You won {wonAmount} today :) Come back soon!", ConsoleColor.White), Times.Once);
    }

    private static void VerifyEncourigingMessage(Money lostAmount)
    {
        WriteLineMock.Verify(x => x.WriteLine($"Thanks for playing! You lost {lostAmount} today :( Better luck next time!", ConsoleColor.White), Times.Once);
    }
}
