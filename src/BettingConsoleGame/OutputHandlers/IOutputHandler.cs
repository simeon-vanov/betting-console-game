using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingConsoleGame.OutputHandlers
{
    public interface IOutputHandler
    {
        public void WriteLine(string message, ConsoleColor consoleColor = ConsoleColor.White);

        public void WriteLine();
    }
}
