using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;

public record WithdrawAction(Money Amount) : IAction;
