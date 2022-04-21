using BettingConsoleGame.Application.Action.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Action.ActionResult;

public record ExitResult(Money WonAmount, ResultType Type) : IActionResult;
