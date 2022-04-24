using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
