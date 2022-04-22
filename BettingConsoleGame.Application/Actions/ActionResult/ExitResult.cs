using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.ActionResult;

public record ExitResult(Money WonAmount, Money LostAmount) : IActionResult;
