using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Exceptions;

public class InvalidBetException : Exception
{
    public InvalidBetException(Money minBet, Money maxBet) : base($"Minimum bet is {minBet} and maximum {maxBet}")
    {

    }
}
