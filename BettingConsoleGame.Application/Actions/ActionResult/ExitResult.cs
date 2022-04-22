using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.Enums;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.ActionResult;

public record ExitResult(Money WonAmount, ResultType Type) : IActionResult;
