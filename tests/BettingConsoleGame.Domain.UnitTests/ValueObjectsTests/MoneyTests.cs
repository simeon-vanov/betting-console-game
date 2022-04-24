using BettingConsoleGame.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace BettingConsoleGame.Domain.UnitTests.ValueObjectsTests;

public class MoneyTests
{
    [Test]
    public void Should_BeEqual_When_TwoAmountsAndCurrenciesAreEqual()
    {
        // Arrange
        var money1 = Money.Dollars(10);
        var money2 = Money.Dollars(10);

        // Act
        var result = money1.Equals(money2);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void Should_NotBeEqual_When_TwoAmountsAreEqualButCurrenciesAreNot()
    {
        // Arrange
        var money1 = new Money(10, new Currency("EUR", "€"));
        var money2 = Money.Dollars(10);

        // Act
        var result = money1.Equals(money2);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void Should_BePossibleToSum_When_UsingPlusOperator()
    {
        // Arrange
        var money1 = Money.Dollars(10);
        var money2 = Money.Dollars(10);

        // Act
        var result = money1 + money2;

        // Assert
        result.Should().Be(Money.Dollars(20));
    }

    [Test]
    public void Should_BePossibleToMultiplyWithInteger_When_UsingMultiplyOperator()
    {
        // Arrange
        var money1 = Money.Dollars(10);

        // Act
        var result = money1 * 2;

        // Assert
        result.Should().Be(Money.Dollars(20));
    }

    [Test]
    public void Should_BePossibleToSubtract_When_UsingMinusOperator()
    {
        // Arrange
        var money1 = Money.Dollars(10);
        var money2 = Money.Dollars(10);

        // Act
        var result = money1 - money2;

        // Assert
        result.Should().Be(Money.ZeroDollars);
    }

    [Test]
    [TestCase(10, 10, false)]
    [TestCase(10, 15, false)]
    [TestCase(15, 10, true)]
    public void Should_BePossibleToUseGreaterThan_When_ComparingMoney(decimal amount1, decimal amount2, bool result)
    {
        // Assert
        (Money.Dollars(amount1) > Money.Dollars(amount2)).Should().Be(result);
    }

    [Test]
    [TestCase(10, 10, false)]
    [TestCase(10, 15, true)]
    [TestCase(15, 10, false)]
    public void Should_BePossibleToUseLessThan_When_ComparingMoney(decimal amount1, decimal amount2, bool result)
    {
        // Assert
        (Money.Dollars(amount1) < Money.Dollars(amount2)).Should().Be(result);
    }

    [Test]
    [TestCase(10, 10, true)]
    [TestCase(10, 10.1, false)]
    [TestCase(15, 10, false)]
    public void Should_BePossibleToUseEqualsSign_When_ComparingMoney(decimal amount1, decimal amount2, bool result)
    {
        // Assert
        (Money.Dollars(amount1) == Money.Dollars(amount2)).Should().Be(result);
    }

    [Test]
    [TestCase(10, 10, false)]
    [TestCase(10, 10.1, true)]
    [TestCase(15, 10, true)]
    public void Should_BePossibleToUseNotEqualsSign_When_ComparingMoney(decimal amount1, decimal amount2, bool result)
    {
        // Assert
        (Money.Dollars(amount1) != Money.Dollars(amount2)).Should().Be(result);
    }
}
