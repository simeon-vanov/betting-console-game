using BettingConsoleGame.Domain.Entities.Games.PlayBets;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.BetAction;
public record BetResult(Money NewBalance, Money WinAmount, BetGameWinnerType WinnerType) : IActionResult;

