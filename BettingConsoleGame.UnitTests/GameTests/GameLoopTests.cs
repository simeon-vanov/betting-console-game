using AutoFixture;
using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Application.Actions.BetAction;
using BettingConsoleGame.Application.Actions.DepositAction;
using BettingConsoleGame.Application.Actions.ExitAction;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.ValueObjects;
using BettingConsoleGame.Game;
using BettingConsoleGame.OutputHandlers;
using BettingConsoleGame.Test.Helpers;
using Moq;
using NUnit.Framework;
using System;

namespace BettingConsoleGame.UnitTests
{
    public class GameLoopTests
    {
        private Mock<IGameActionHandler> gameActionHandlerMock;
        private Mock<IActionResultOutputter> actionResultOutputterMock;
        private GameLoop sut;

        [SetUp]
        public void Setup()
        {
            this.gameActionHandlerMock = new Mock<IGameActionHandler>();
            this.actionResultOutputterMock = new Mock<IActionResultOutputter>();
            this.sut = new GameLoop(this.gameActionHandlerMock.Object, actionResultOutputterMock.Object);
        }

        [Test]
        public void Should_ContinueAsikngForAction_When_UntilExit()
        {
            // Arrange
            var wallet = Wallet.Empty;
            var depositResult= FixtureFactory.CreateFixture().Create<DepositResult>();
            var betResult = FixtureFactory.CreateFixture().Create<BetResult>();
            var exitResult = FixtureFactory.CreateFixture().Create<ExitResult>();
            
            this.gameActionHandlerMock
                .SetupSequence(x=> x.Execute(wallet))
                .Returns(Result<IActionResult>.Succeed(depositResult))
                .Returns(Result<IActionResult>.Succeed(betResult))
                .Returns(Result<IActionResult>.Succeed(exitResult));

            // Act
            this.sut.Start(wallet);

            // Assert
            gameActionHandlerMock.Verify(x => x.Execute(wallet), Times.Exactly(3));
        }

        [Test]
        public void Should_OutputSomethingWentWrongThenContinueWithNextAction_When_NotCaughtExceptionIsThrown()
        {
            // Arrange
            var wallet = Wallet.Empty;

            var exitResult = FixtureFactory.CreateFixture().Create<ExitResult>();

            this.gameActionHandlerMock
                .SetupSequence(x => x.Execute(wallet))
                .Throws(new NullReferenceException())
                .Returns(Result<IActionResult>.Succeed(exitResult));

            // Act
            this.sut.Start(wallet);

            // Assert
            actionResultOutputterMock.Verify(x => x.OutputError("Something went wrong."));
            gameActionHandlerMock.Verify(x => x.Execute(wallet), Times.Exactly(2));
        }

        [Test]
        public void Should_ContinueToNextActionAndNotShowSomethingWentWrong_When_ActionFailedWithoutException()
        {
            // Arrange
            var wallet = Wallet.Empty;

            var depositSuccess = FixtureFactory.CreateFixture().Create<DepositResult>();
            var exitResult = FixtureFactory.CreateFixture().Create<ExitResult>();

            this.gameActionHandlerMock
                .SetupSequence(x => x.Execute(wallet))
                .Returns(Result<IActionResult>.Fail("failed"))
                .Returns(Result<IActionResult>.Succeed(depositSuccess))
                .Returns(Result<IActionResult>.Succeed(exitResult));

            // Act
            this.sut.Start(wallet);

            // Assert
            actionResultOutputterMock.Verify(x => x.OutputError("Something went wrong."), Times.Never);
            gameActionHandlerMock.Verify(x => x.Execute(wallet), Times.Exactly(3));
        }
    }
}