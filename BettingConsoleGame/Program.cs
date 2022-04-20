using BettingConsoleGame;
using BettingConsoleGame.ActionParsers;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Services;
using BettingConsoleGame.Domain.Services.Randomize;
using BettingConsoleGame.InputOutputHandlers;

var gameEnvironment = new GameLoop(
    new ConsoleActionReader(),
    new ConsoleOutputter(),
    new GameService(new NumberRandomizerService()));

gameEnvironment.Start(Wallet.Empty);