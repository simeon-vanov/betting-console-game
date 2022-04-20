using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities.GameEnvironment.ActionResult;

public record WithdrawResult(Money NewBalance, Money Deposited, ResultType Type) : IActionResult;
