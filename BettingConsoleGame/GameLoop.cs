﻿using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Entities.Action.Types;
using BettingConsoleGame.Domain.Exceptions;
using BettingConsoleGame.Domain.Services;
using BettingConsoleGame.Exceptions;
using BettingConsoleGame.InputOutputHandlers;

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
                actionResultOutputter.OutputMessage("Invalid parameters for the action: " + exception.Message);
            }
            catch(UnknownActionException exception)
            {
                actionResultOutputter.OutputMessage($"{exception.Message}. Please use deposit, withdraw, bet or exit actions.");
            }
            catch (InvalidBetException exception)
            {
                actionResultOutputter.OutputMessage($"{exception.Message}.");
            }
            catch (NotEnoughMoneyException)
            {
                actionResultOutputter.OutputMessage($"Not enough balance to execute the action.");
            }
            catch (Exception)
            {
                actionResultOutputter.OutputMessage($"Something went wrong.");
            }
        }
    }
}
