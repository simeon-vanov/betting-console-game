using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Entities.GameEnvironment;
using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;
using BettingConsoleGame.Domain.Exceptions;
using BettingConsoleGame.Domain.Services;
using BettingConsoleGame.Exceptions;

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
            try
            {
                var action = actionReader.GetNextAction();
                var result = gameService.Execute(action, wallet);

                actionResultOutputter.Output(result);

                if (action.GetType() == typeof(ExitAction))
                {
                    return;
                }
            }
            catch(InvalidActionParametersException exception)
            {
                Console.WriteLine("Invalid parameters for the action: " + exception.Message);
            }
            catch(UnknownActionException exception)
            {
                Console.WriteLine($"{exception.Message}. Please use deposit, withdraw, bet or exit actions.");
            }
            catch (InvalidBetException exception)
            {
                Console.WriteLine($"{exception.Message}.");
            }
            catch (NotEnoughMoneyException)
            {
                Console.WriteLine($"Not enough balance to execute the action. Please deposit more funds.");
            }
            catch (Exception)
            {
                Console.WriteLine($"Something went wrong.");
            }

            Console.WriteLine();
        }
    }
}
