namespace BettingConsoleGame.Domain.Exceptions;

public class NotEnoughMoneyException : Exception
{
    public NotEnoughMoneyException() : base()
    {
        
    }

    public NotEnoughMoneyException(string? message) : base(message)
    {
    }
}
