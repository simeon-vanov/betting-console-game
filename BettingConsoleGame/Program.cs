using BettingConsoleGame;
using BettingConsoleGame.Application.Actions.Factory;
using BettingConsoleGame.Application.Services.Randomize;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Factories;
using BettingConsoleGame.InputOutputHandlers;

var gameEnvironment = new GameLoop(
    new ConsoleActionReader(new ActionFactory(new GameFactory(new NumberRandomizerService()))),
    new ConsoleOutputter());

gameEnvironment.Start(Wallet.Empty);