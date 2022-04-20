using BettingConsoleGame;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Entities.GameEnvironment;
using BettingConsoleGame.Domain.Services.Randomize;


var gameEnvironment = new GameLoop(
    new ConsoleActionReader(new ConsoleActionParserFactory()),
    new ConsoleActionOutputResult(),
    new GameService(new NumberRandomizerService()));

gameEnvironment.Start(Wallet.EmptyWallet);