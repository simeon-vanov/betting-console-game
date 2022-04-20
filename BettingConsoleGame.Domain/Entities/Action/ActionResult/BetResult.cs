using BettingConsoleGame.Domain.Entities.SlotGame;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities.Action.ActionResult;
public record BetResult(Money NewBalance, Money WinAmount, BetGameWinnerType WinnerType, ResultType Type) : IActionResult;

