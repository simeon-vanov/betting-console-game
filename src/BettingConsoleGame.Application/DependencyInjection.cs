using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Application.Actions.Factory;
using BettingConsoleGame.Application.Services.Randomize;
using BettingConsoleGame.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BettingConsoleGame.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<INumberRandomizerService, NumberRandomizerService>();
        services.AddScoped<IActionFactory, ActionFactory>();


        return services;
    }
}
