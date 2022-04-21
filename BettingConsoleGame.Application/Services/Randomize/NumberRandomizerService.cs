using BettingConsoleGame.Domain.Services;

namespace BettingConsoleGame.Application.Services.Randomize;

public class NumberRandomizerService : INumberRandomizerService
{
    public double GetRandomDouble(double min = 0, double max = 1)
    {
        return NumberRandomizer.GetRandomDouble(min, max);
    }
}
