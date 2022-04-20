using BettingConsoleGame.Domain.Entities.GameEnvironment.Actions;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities.GameEnvironment.ActionResult;

public record ExitResult(Money WonAmount, ResultType Type) : IActionResult;
