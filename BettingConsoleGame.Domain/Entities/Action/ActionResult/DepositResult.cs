using BettingConsoleGame.Domain.Entities.Action;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities.Action.ActionResult;

public record DepositResult(Money NewBalance, Money Deposited, ResultType Type) : IActionResult;
