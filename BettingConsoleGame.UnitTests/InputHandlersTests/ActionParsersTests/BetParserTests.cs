using AutoFixture;
using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Application.Actions.BetAction;
using BettingConsoleGame.Domain.ValueObjects;
using BettingConsoleGame.InputHandlers.ActionParsers;
using BettingConsoleGame.Test.Helpers;
using Moq;
using NUnit.Framework;

namespace BettingConsoleGame.UnitTests.InputHandlersTests.ActionParsersTests;

public class BetParserTests : ActionWithAmountParserTests<Bet>
{
    private Mock<IActionFactory> actionFactoryMock;
    
    [SetUp]
    public void Setup()
    {
        actionFactoryMock = new Mock<IActionFactory>();
        actionFactoryMock
            .Setup(x => x.CreateBetAction(It.IsAny<Money>()))
            .Returns(FixtureFactory
                .CreateAutoMoqFixture()
                .Create<Bet>());

        sut = new BetParser(actionFactoryMock.Object);
    }
}
