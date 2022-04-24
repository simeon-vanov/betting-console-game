using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.InputHandlers;
using BettingConsoleGame.InputHandlers.ConsoleInput;
using BettingConsoleGame.OutputHandlers;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;

namespace BettingConsoleGame.UnitTests.InputHandlersTests.ConsoleInputTests;

public class ActionReaderTests
{
    private Mock<IActionFactory> actionFactoryMock;
    private Mock<IOutputHandler> outputHandlerMock;
    private Mock<IInputHandler> inputHandlerMock;
    
    private ActionReader sut;

    [SetUp]
    public void Setup()
    {
        actionFactoryMock = new Mock<IActionFactory>();
        outputHandlerMock = new Mock<IOutputHandler>();
        inputHandlerMock = new Mock<IInputHandler>();
        
        sut = new ActionReader(actionFactoryMock.Object, inputHandlerMock.Object, outputHandlerMock.Object);
    }

    [Test]
    public void ReturnFailResultWithKnownActionsWhenInputIsEmpty()
    {
        // Arrange
        inputHandlerMock.Setup(x => x.ReadLine()).Returns(string.Empty);
        
        // Act
        var result = sut.GetNextAction();

        // Assert
        result.Failed.Should().BeTrue();
        result.Errors.Should().ContainSingle("Known actions are: deposit, withdraw, bet and exit");
    }

    [Test]
    public void OutputPleaseSubmitActionEachTimeActionIsNeeded()
    {
        // Arrange
        inputHandlerMock.Setup(x => x.ReadLine()).Returns(string.Empty);
        outputHandlerMock.Setup(x => x.WriteLine(It.IsAny<string>(), It.IsAny<ConsoleColor>()));
        
        // Act
        sut.GetNextAction();
        sut.GetNextAction();
        sut.GetNextAction();

        // Assert
        outputHandlerMock.Verify(x => x.WriteLine("Please, Submit Action: ", ConsoleColor.White), Times.Exactly(3));
    }
}
