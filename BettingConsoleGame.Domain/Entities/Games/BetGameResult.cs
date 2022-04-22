using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities.Games;

public record BetGameResult(Money WinAmount, BetGameWinnerType WinnerType);