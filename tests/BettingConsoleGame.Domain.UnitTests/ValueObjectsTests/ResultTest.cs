using AutoFixture;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.ValueObjects;
using BettingConsoleGame.Test.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace BettingConsoleGame.Domain.UnitTests.ValueObjectsTests;

public class ResultTest
{
    [Test]
    public void Should_SetFailedCorrectly_When_SingleErrorFailResult()
    {
        var result = Result<Money>.Fail("Error");

        result.Errors.Should().ContainSingle("Error");
        result.Failed.Should().BeTrue();
        result.Succeeded.Should().BeFalse();
        result.Value.Should().BeNull();
    }

    [Test]
    public void Should_SetFailedCorrectly_When_MultipleErrorFailResult()
    {
        var result = Result<Money>.Fail(new string[] { "Error", "Error 2" });

        result.Errors.Should().Contain("Error");
        result.Errors.Should().Contain("Error 2");
        result.Errors.Should().HaveCount(2);

        result.Failed.Should().BeTrue();
        result.Succeeded.Should().BeFalse();
        result.Value.Should().BeNull();
    }

    [Test]
    public void Should_SetSucceededCorrectly_When_Succeed()
    {
        var value = FixtureFactory.CreateFixture().Create<Wallet>();
        var result = Result<Wallet>.Succeed(value);

        result.Errors.Should().BeEmpty();
        result.Failed.Should().BeFalse();
        result.Succeeded.Should().BeTrue();
        result.Value.Should().Be(value);
    }
}
