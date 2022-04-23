using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.ExitAction;

public record ExitResult(Money WonAmount, Money LostAmount) : IActionResult;
