using BettingConsoleGame.Application.Action.Interfaces;
using BettingConsoleGame.Domain.Entities.SlotGame;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Action.ActionResult;
public record BetResult(Money NewBalance, Money WinAmount, BetGameWinnerType WinnerType, ResultType Type) : IActionResult;

