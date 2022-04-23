using BettingConsoleGame.Application.Actions.ExitAction;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Enums;
using BettingConsoleGame.InputHandlers;
using BettingConsoleGame.OutputHandlers;

namespace BettingConsoleGame.Game;

public class GameLoop
{
    private readonly IActionReader actionReader;
    private readonly IActionResultOutputter actionResultOutputter;

    public GameLoop(IActionReader actionReader, IActionResultOutputter actionResultOutputter)
    {
        this.actionReader = actionReader;
        this.actionResultOutputter = actionResultOutputter;
    }

    public void Start(Wallet wallet)
    {
        while (true)
        {
            try
            {
                var actionParseResult = actionReader.GetNextAction();

                if (actionParseResult.Failed)
                {
                    actionResultOutputter.OutputError(actionParseResult.Errors);
                    continue;
                }

                var action = actionParseResult.Value;
                var result = action.Execute(wallet);

                actionResultOutputter.Output(result);

                if (action.GetType() == typeof(Exit))
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
