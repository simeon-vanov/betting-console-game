using BettingConsoleGame.Application.Action.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Action.Types;

public record DepositAction(Money Amount) : IAction;
