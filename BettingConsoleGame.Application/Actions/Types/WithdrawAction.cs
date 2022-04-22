using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.Types;

public record WithdrawAction(Money Amount) : IAction;
