using BettingConsoleGame.Domain.Factories;
using BettingConsoleGame.Domain.Factories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BettingConsoleGame.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IGameFactory, GameFactory>();

        return services;
    }
}
