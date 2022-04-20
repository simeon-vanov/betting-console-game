using BettingConsoleGame.Domain.Entities.Action;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities.Action.ActionResult;

public record ExitResult(Money WonAmount, ResultType Type) : IActionResult;
