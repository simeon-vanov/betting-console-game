using BettingConsoleGame;
using BettingConsoleGame.Application.Services;
using BettingConsoleGame.Application.Services.Randomize;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.InputOutputHandlers;

var gameEnvironment = new GameLoop(
    new ConsoleActionReader(),
    new ConsoleOutputter(),
    new GameService(new NumberRandomizerService()));

gameEnvironment.Start(Wallet.Empty);