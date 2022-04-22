using BettingConsoleGame.Application.Actions.Types;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Enums;
using BettingConsoleGame.Interfaces;

namespace BettingConsoleGame;

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

                if(actionParseResult.ResultType == ResultType.Fail)
                {
                    actionResultOutputter.OutputError(actionParseResult.Errors);
                    continue;
                }

                var action = actionParseResult.ResultItem;
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
