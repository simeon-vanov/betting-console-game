using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.Entities.Games;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.ActionResult;
public record BetResult(Money NewBalance, Money WinAmount, BetGameWinnerType WinnerType) : IActionResult;

