using AutoFixture;
using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Application.Actions.ExitAction;
using BettingConsoleGame.InputHandlers;
using BettingConsoleGame.InputHandlers.ActionParsers;
using BettingConsoleGame.Test.Helpers;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BettingConsoleGame.UnitTests.InputHandlersTests.ActionParsersTests;

public class ExitParserTests
{
    private Mock<IActionFactory> actionFactoryMock;
    private IActionParser sut;

    [SetUp]
    public void Setup()
    {
        actionFactoryMock = new Mock<IActionFactory>();
        actionFactoryMock
            .Setup(x => x.CreateExitAction())
            .Returns(FixtureFactory
                .CreateAutoMoqFixture()
                .Create<Exit>());

        sut = new ExitParser(actionFactoryMock.Object);
    }

    [Test]
    public void Parse_ShouldReturnExitAction_WhenValidAction()
    {
        // Arrange
        var input = "exit";

        // Act
        var result = sut.Parse(new string[] { input });

        // Assert
        result.Value.Should().BeAssignableTo<Exit>();
    }

    [Test]
    public void Parse_ShouldReturnFaile_WhenMultipleArguments()
    {
        // Arrange
        var input = "exit";

        // Act
        var result = sut.Parse(new string[] { input, "10" });

        // Assert
        result.Value.Should().BeNull();
        result.Failed.Should().BeTrue();
        result.Errors.Should().ContainSingle("Exit action does not accept any parameters.");
    }
}
