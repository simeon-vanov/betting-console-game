using AutoFixture;
using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Application.Actions.BetAction;
using BettingConsoleGame.Application.Actions.DepositAction;
using BettingConsoleGame.Application.Actions.WithdrawAction;
using BettingConsoleGame.Domain.ValueObjects;
using BettingConsoleGame.InputHandlers.ActionParsers;
using BettingConsoleGame.Test.Helpers;
using Moq;
using NUnit.Framework;

namespace BettingConsoleGame.UnitTests.InputHandlersTests.ActionParsersTests;

public class WithdrawParserTests : ActionWithAmountParserTests<Withdraw>
{
    private Mock<IActionFactory> actionFactoryMock;
    
    [SetUp]
    public void Setup()
    {
        actionFactoryMock = new Mock<IActionFactory>();
        actionFactoryMock
            .Setup(x => x.CreateWithdrawAction(It.IsAny<Money>()))
            .Returns(FixtureFactory
                .CreateAutoMoqFixture()
                .Create<Withdraw>());

        sut = new WithdrawParser(actionFactoryMock.Object);
    }
}
