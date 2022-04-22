using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.Entities.SlotGame;
using BettingConsoleGame.Domain.Enums;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.ActionResult;
public record BetResult(Money NewBalance, Money WinAmount, BetGameWinnerType WinnerType, ResultType Type) : IActionResult;

