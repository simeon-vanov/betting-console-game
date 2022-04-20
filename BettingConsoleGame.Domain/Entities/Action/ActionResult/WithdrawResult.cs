using BettingConsoleGame.Domain.Entities.Action;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities.Action.ActionResult;

public record WithdrawResult(Money NewBalance, Money Deposited, ResultType Type) : IActionResult;
