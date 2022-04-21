using BettingConsoleGame.Application.Action.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Action.ActionResult;

public record DepositResult(Money NewBalance, Money Deposited, ResultType Type) : IActionResult;
