using BettingConsoleGame.Application.Actions.ExitAction;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Enums;
using BettingConsoleGame.InputHandlers;
using BettingConsoleGame.OutputHandlers;

namespace BettingConsoleGame.Game;

public class GameLoop
{
    private readonly GameActionHandler gameActionHandler;
    private readonly IActionResultOutputter actionResultOutputter;

    public GameLoop(GameActionHandler gameActionHandler, IActionResultOutputter actionResultOutputter)
    {
        this.gameActionHandler = gameActionHandler;
        this.actionResultOutputter = actionResultOutputter;
    }

    public void Start(Wallet wallet)
    {
        while (true)
        {
            try
            {
                var actionResult = gameActionHandler.Execute(wallet);

                if (actionResult.Succeeded && 
                    actionResult.Value.GetType() == typeof(ExitResult))
                {
                    return;
                }
            }
            catch (Exception)
            {
                actionResultOutputter.OutputError($"Something went wrong.");
            }
        }
    }
}
