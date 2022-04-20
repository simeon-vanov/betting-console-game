namespace BettingConsoleGame.Domain.Services.Randomize;

public interface INumberRandomizerService
{
    public double GetRandomDouble(double min = 0, double max = 1);
}
