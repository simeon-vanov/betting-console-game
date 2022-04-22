using BettingConsoleGame;
using BettingConsoleGame.Application;
using BettingConsoleGame.Domain;
using BettingConsoleGame.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

IServiceCollection services = new ServiceCollection();

services.AddDomain();
services.AddApplication();
services.AddConsole();

var serviceProvider = services.BuildServiceProvider();
var gameLoop = serviceProvider.GetService<GameLoop>();

gameLoop.Start(Wallet.Empty);