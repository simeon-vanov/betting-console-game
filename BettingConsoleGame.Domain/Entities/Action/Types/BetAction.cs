using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities.Action.Types;

public record BetAction(Money Amount) : IAction;
