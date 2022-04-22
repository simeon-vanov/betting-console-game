using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.ActionResult;

public record WithdrawResult(Money NewBalance, Money Deposited) : IActionResult;
