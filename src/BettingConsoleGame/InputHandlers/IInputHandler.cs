using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingConsoleGame.InputHandlers
{
    public interface IInputHandler
    {
        public string ReadLine { get; set; }
    }
}
