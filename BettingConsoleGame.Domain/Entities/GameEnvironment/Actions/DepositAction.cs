using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;

public record DepositAction(Money Amount) : IAction;
