namespace BettingConsoleGame.Domain.Services.Randomize;

public static class NumberRandomizer
{
    private static Random Random = new Random();

    public static double GetRandomDouble(double min, double max)
    {
        // just a math formula to be generate numbers in a certain range instead of 0 to 1
        return Random.NextDouble() * (max - min) + min;
    }
}
