using BettingConsoleGame.InputOutputHandlers;
using BettingConsoleGame.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
