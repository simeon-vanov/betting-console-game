using NUnit.Framework;

namespace BettingConsoleGame.IntegrationTests;

using static Testing;


public class TestBase
{
    [SetUp]
    public void RunBeforeEachTest()
    {
        ResetMocks();
    }
}
