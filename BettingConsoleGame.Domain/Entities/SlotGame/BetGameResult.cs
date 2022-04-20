using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Domain.Entities.SlotGame;

public record BetGameResult(Money WinAmount, BetGameWinnerType WinnerType);