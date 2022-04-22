using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.Types;

public record DepositAction(Money Amount) : IAction;
