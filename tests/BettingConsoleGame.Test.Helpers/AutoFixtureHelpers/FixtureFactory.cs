using AutoFixture;
using AutoFixture.AutoMoq;

namespace BettingConsoleGame.Test.Helpers
{
    public static class FixtureFactory
    {
        public static Fixture CreateAutoMoqFixture()
        {
            Fixture? fixture = new();
            fixture.Customize(new AutoMoqCustomization());

            return fixture;
        }

        public static Fixture CreateFixture()
        {
            Fixture? fixture = new();
            
            return fixture;
        }
    }

}