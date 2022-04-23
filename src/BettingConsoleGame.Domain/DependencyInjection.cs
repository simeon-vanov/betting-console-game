using BettingConsoleGame.Domain.Entities.Games;
using BettingConsoleGame.Domain.Entities.Games.Factory;
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
