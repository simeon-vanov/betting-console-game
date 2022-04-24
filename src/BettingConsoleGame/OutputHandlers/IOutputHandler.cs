namespace BettingConsoleGame.OutputHandlers
{
    public interface IOutputHandler
    {
        public void WriteLine(string message, ConsoleColor consoleColor = ConsoleColor.White);

        public void WriteLine();
    }
}
