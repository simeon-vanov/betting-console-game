using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Entities.GameEnvironment;
using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;
using BettingConsoleGame.Domain.Services;

namespace BettingConsoleGame;

public class GameLoop
{
    private readonly IActionReader actionReader;
    private readonly IActionResultOutputter actionResultOutputter;
    private readonly IGameService gameService;

    public GameLoop(IActionReader actionReader, IActionResultOutputter actionResultOutputter, IGameService gameService)
    {
        this.actionReader = actionReader;
        this.actionResultOutputter = actionResultOutputter;
        this.gameService = gameService;
    }

    public void Start(Wallet wallet)
    {
        while (true)
        {
            var action = actionReader.GetNextAction();
            var result = gameService.Execute(action, wallet);

            actionResultOutputter.Output(result);

            if (action.GetType() != typeof(ExitAction))
            {
                return;
            }
        }
    }
}
