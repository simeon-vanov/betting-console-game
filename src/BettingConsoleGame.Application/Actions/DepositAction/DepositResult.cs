using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.DepositAction;

public record DepositResult(Money NewBalance, Money Deposited) : IActionResult;
