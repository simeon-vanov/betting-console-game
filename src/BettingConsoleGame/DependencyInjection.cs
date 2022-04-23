using BettingConsoleGame.Game;
using BettingConsoleGame.InputHandlers;
using BettingConsoleGame.InputHandlers.ConsoleInput;
using BettingConsoleGame.OutputHandlers;
using BettingConsoleGame.OutputHandlers.ConsoleOutput;
using Microsoft.Extensions.DependencyInjection;

namespace BettingConsoleGame;

public static class DependencyInjection
{
    public static IServiceCollection AddConsole(this IServiceCollection services)
    {
        services.AddScoped<GameLoop>();
        services.AddScoped<GameActionHandler>();
        services.AddScoped<IActionReader, ActionReader>();
        services.AddScoped<IActionResultOutputter, ActionOutputter>();
        services.AddScoped<IInputHandler, ConsoleReadLine>();
        services.AddScoped<IOutputHandler, ConsoleWriteLine>();

        return services;
    }
}
