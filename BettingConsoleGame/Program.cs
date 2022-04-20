using BettingConsoleGame;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Entities.GameEnvironment;
using BettingConsoleGame.Domain.Services.Randomize;
using BettingConsoleGame.InputOutputHandlers;

var gameEnvironment = new GameLoop(
    new ConsoleActionReader(new ConsoleActionParserFactory()),
    new ConsoleOutputter(),
    new GameService(new NumberRandomizerService()));

gameEnvironment.Start(Wallet.Empty);