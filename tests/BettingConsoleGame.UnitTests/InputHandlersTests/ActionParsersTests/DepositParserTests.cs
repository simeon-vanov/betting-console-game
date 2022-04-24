using AutoFixture;
using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Application.Actions.DepositAction;
using BettingConsoleGame.Domain.ValueObjects;
using BettingConsoleGame.InputHandlers.ActionParsers;
using BettingConsoleGame.Test.Helpers;
using Moq;
using NUnit.Framework;

namespace BettingConsoleGame.UnitTests.InputHandlersTests.ActionParsersTests;

public class DepositParserTests : ActionWithAmountParserTests<Deposit>
{
    private Mock<IActionFactory> actionFactoryMock;
    
    [SetUp]
    public void Setup()
    {
        actionFactoryMock = new Mock<IActionFactory>();
        actionFactoryMock
            .Setup(x => x.CreateDepositAction(It.IsAny<Money>()))
            .Returns(FixtureFactory
                .CreateAutoMoqFixture()
                .Create<Deposit>());

        sut = new DepositParser(actionFactoryMock.Object);
    }
}
