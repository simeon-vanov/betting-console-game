using BettingConsoleGame.InputHandlers;
using BettingConsoleGame.InputHandlers.ActionParsers;
using FluentAssertions;
using NUnit.Framework;

namespace BettingConsoleGame.UnitTests.InputHandlersTests.ActionParsersTests;

public abstract class ActionWithAmountParserTests<T>
{
    protected IActionParser sut;
    
    protected string actionParameter;

    [Test]
    [TestCase("10")]
    [TestCase("10.05")]
    [TestCase("10.52")]
    [TestCase("$10")]
    [TestCase("10$")]
    [TestCase("10.55$")]
    public void Parse_ShouldReturnAction_WhenValidAmount(string amount)
    {
        // Act
        var actionParser = sut.Parse(new string[] { actionParameter, amount });

        // Assert
        actionParser.Succeeded.Should().BeTrue();
        actionParser.Value.Should().BeAssignableTo<T>();
    }

    [Test]
    [TestCase("10invalid", "Amount must be in the format 0.00.")]
    [TestCase("-10", "Amount must be positive number bigger than 0.")]
    [TestCase("0", "Amount must be positive number bigger than 0.")]
    [TestCase("10.234", "Amount must be in the format 0.00.")]
    public void Parse_ShouldReturnFail_WhenInvalidAmountOrNotPositive(string amount, string validationMessage)
    {
        // Act
        var actionParser = sut.Parse(new string[] { actionParameter, amount });

        // Assert
        actionParser.Failed.Should().BeTrue();
        actionParser.Errors.Should().ContainSingle(validationMessage);
        actionParser.Value.Should().BeNull();
    }

    [Test]
    public void Parse_ShouldReturnFail_WhenMultipleArguments()
    {
        // Act
        var actionParser = sut.Parse(new string[] { actionParameter, "10", "20" });

        // Assert
        actionParser.Failed.Should().BeTrue();
        actionParser.Value.Should().BeNull();
        actionParser.Errors.Should().ContainSingle("Action accepts only amount as parameter.");
    }
}
