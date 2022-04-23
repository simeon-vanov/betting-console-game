using BettingConsoleGame.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingConsoleGame.IntegrationTests.Mocks;

public class NumberRandomizerServiceMock
{
    public Mock<INumberRandomizerService> Mock;

    public NumberRandomizerServiceMock()
    {
        Mock = new Mock<INumberRandomizerService>();
    }

    public void SetupChanceWinner(double returnedChance = 5)
    {
        SetupGetRandomDouble(0, 100, returnedChance);
    }

    public void SetupBigWinnerMultiplier(double returnedMultiplier)
    {
        SetupGetRandomDouble(2, 10, returnedMultiplier);
    }

    public void SetupWinnerMultiplier(double returnedMultiplier)
    {
        SetupGetRandomDouble(1, 2, returnedMultiplier);
    }

    public void SetupGetRandomDouble(double min, double max, double returnedValue = 2)
    {
        Mock.Setup(x => x.GetRandomDouble(min, max)).Returns(returnedValue);
    }
}
