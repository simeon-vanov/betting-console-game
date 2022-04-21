namespace BettingConsoleGame.Domain.Services;

public interface INumberRandomizerService
{
    public double GetRandomDouble(double min = 0, double max = 1);
}
