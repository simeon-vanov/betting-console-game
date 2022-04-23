namespace BettingConsoleGame.InputHandlers.ConsoleInput;

public class ConsoleReadLine : IInputHandler
{
    public string ReadLine()
    {
        return Console.ReadLine();
    }
}
