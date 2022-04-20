using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities.Action.Types;

public record WithdrawAction(Money Amount) : IAction;
