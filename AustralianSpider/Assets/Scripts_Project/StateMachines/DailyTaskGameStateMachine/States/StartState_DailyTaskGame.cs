using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState_DailyTaskGame : IState
{
    private StoreGameDesignPresenter storeGameDesignPresenter;
    private StoreCoverCardDesignPresenter storeCoverCardDesignPresenter;
    private StoreFaceCardDesignPresenter storeFaceCardDesignPresenter;
    private StoreGameTypePresenter storeGameTypePresenter;
    private StoreCardPresenter storeCardPresenter;
    private GameDesignPresenter gameDesignPresenter;
    private CardColumnPresenter cardColumnPresenter;
    private TimerPresenter timerPresenter;

    private IGlobalStateMachine stateMachine;

    public StartState_DailyTaskGame(
        IGlobalStateMachine stateMachine,
        StoreGameDesignPresenter storeGameDesignPresenter,
        StoreCoverCardDesignPresenter storeCoverCardDesignPresenter,
        StoreFaceCardDesignPresenter storeFaceCardDesignPresenter,
        StoreGameTypePresenter storeGameTypePresenter,
        StoreCardPresenter storeCardPresenter,
        GameDesignPresenter gameDesignPresenter,
        CardColumnPresenter cardColumnPresenter,
        TimerPresenter timerPresenter)
    {
        this.stateMachine = stateMachine;
        this.storeGameDesignPresenter = storeGameDesignPresenter;
        this.storeCoverCardDesignPresenter = storeCoverCardDesignPresenter;
        this.storeFaceCardDesignPresenter = storeFaceCardDesignPresenter;
        this.storeGameTypePresenter = storeGameTypePresenter;
        this.storeCardPresenter = storeCardPresenter;
        this.gameDesignPresenter = gameDesignPresenter;
        this.cardColumnPresenter = cardColumnPresenter;
        this.timerPresenter = timerPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - START");

        storeCardPresenter.CreateCards();
        storeCardPresenter.DealCards();
        timerPresenter.ActivateTimer();

        ChangeStateToMain();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - START");
    }

    private void ChangeStateToMain()
    {
        stateMachine.SetState(stateMachine.GetState<MainState_DailyTaskGame>());
    }
}
