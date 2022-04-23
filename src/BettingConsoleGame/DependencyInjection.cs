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
        services.AddScoped<IActionReader, ConsoleActionReader>();
        services.AddScoped<IActionResultOutputter, ConsoleOutputter>();

        return services;
    }
}
