using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities.Games.PlayBets;

public record BetGameResult(Money WinAmount, BetGameWinnerType WinnerType);