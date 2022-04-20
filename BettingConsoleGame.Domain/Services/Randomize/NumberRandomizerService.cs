namespace BettingConsoleGame.Domain.Services.Randomize;

public class NumberRandomizerService : INumberRandomizerService
{
    public double GetRandomDouble(double min = 0, double max = 1)
    {
        return NumberRandomizer.GetRandomDouble(min, max);
    }
}
