namespace BettingConsoleGame.Domain.Exceptions;

public class UnknownActionException : Exception
{
    public UnknownActionException(string action) : base($"Action {action} is not known")
    {
    }
}
