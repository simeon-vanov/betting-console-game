using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities.Action.Types;

public record DepositAction(Money Amount) : IAction;
