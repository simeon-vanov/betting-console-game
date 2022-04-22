using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.WithdrawAction;

public record WithdrawResult(Money NewBalance, Money Deposited) : IActionResult;
