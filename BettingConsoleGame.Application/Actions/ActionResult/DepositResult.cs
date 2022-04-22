using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Domain.Enums;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.ActionResult;

public record DepositResult(Money NewBalance, Money Deposited, ResultType Type) : IActionResult;
