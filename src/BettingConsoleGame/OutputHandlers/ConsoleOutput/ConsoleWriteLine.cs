namespace BettingConsoleGame.OutputHandlers.ConsoleOutput;

public class ConsoleWriteLine : IOutputHandler
{
    public void WriteLine(string message, ConsoleColor consoleColor = ConsoleColor.White)
    {
        Console.ForegroundColor = consoleColor;
        Console.WriteLine(message);
    }

    public void WriteLine()
    {
        Console.WriteLine();
    }
}
