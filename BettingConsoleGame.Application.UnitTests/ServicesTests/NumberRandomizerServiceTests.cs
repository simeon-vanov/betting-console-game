using BettingConsoleGame.Application.Services.Randomize;
using FluentAssertions;
using NUnit.Framework;

namespace BettingConsoleGame.Application.UnitTests.Services
{
    public class NumberRandomizerServiceTests
    {
        private NumberRandomizerService sut;

        [SetUp]
        public void Setup()
        {
            this.sut = new NumberRandomizerService();
        }

        [Test]
        public void Should_NotDuplicateRandomDouble_When_CalledTwice()
        {
            var first = this.sut.GetRandomDouble();
            var second = this.sut.GetRandomDouble();

            first.Should().NotBe(second);
        }

        [Test]
        public void Should_ReturnRandomDoubleInRange_When_MinMaxIsSpecified()
        {
            var first = this.sut.GetRandomDouble(50, 55);

            first.Should().BeGreaterThanOrEqualTo(50);
            first.Should().BeLessThan(55);
        }
    }
}
