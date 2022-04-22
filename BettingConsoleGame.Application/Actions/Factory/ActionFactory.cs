using BettingConsoleGame.Application.Actions.Interfaces;
using BettingConsoleGame.Application.Actions.Types;
using BettingConsoleGame.Domain.Factories.Interfaces;
using BettingConsoleGame.Domain.ValueObjects;

namespace BettingConsoleGame.Application.Actions.Factory;

public class ActionFactory : IActionFactory
{
    private readonly IGameFactory gameFactory;

    public ActionFactory(IGameFactory gameFactory)
    {
        this.gameFactory = gameFactory;
    }

    public IAction CreateBetAction(Money bet)
    {
        return new Bet(bet, gameFactory);
    }

    public IAction CreateDepositAction(Money deposit)
    {
        return new Deposit(deposit);
    }

    public IAction CreateWithdrawAction(Money withdraw)
    {
        return new Withdraw(withdraw);
    }

    public IAction CreateExitAction()
    {
        return new Exit();
    }
}

