using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskGameStateMachine : IGlobalStateMachine
{
    private Dictionary<Type, IState> states = new Dictionary<Type, IState>();

    private IState currentState;

    public DailyTaskGameStateMachine(
        StoreGameDesignPresenter storeGameDesignPresenter,
        StoreCoverCardDesignPresenter storeCoverCardDesignPresenter,
        StoreFaceCardDesignPresenter storeFaceCardDesignPresenter,
        StoreGameTypePresenter storeGameTypePresenter,
        StoreCardPresenter storeCardPresenter,
        GameDesignPresenter gameDesignPresenter,
        CardColumnPresenter cardColumnPresenter,
        UIDailyTaskGameSceneRoot sceneRoot,
        ScorePresenter scorePresenter,
        MotionCounterPresenter motionCounterPresenter,
        CardMotionHistoryPresenter cardMotionHistoryPresenter,
        TimerPresenter timerPresenter,
        MotionHintPresenter motionHintPresenter,
        StoreDailyTaskPresenter storeDailyTaskPresenter)
    {
        states[typeof(StartState_DailyTaskGame)] = new StartState_DailyTaskGame(this, storeGameDesignPresenter, storeCoverCardDesignPresenter, storeFaceCardDesignPresenter, storeGameTypePresenter, storeCardPresenter, gameDesignPresenter, cardColumnPresenter, timerPresenter);
        states[typeof(MainState_DailyTaskGame)] = new MainState_DailyTaskGame(this, sceneRoot, cardColumnPresenter, storeCardPresenter, scorePresenter, motionCounterPresenter, cardMotionHistoryPresenter, timerPresenter, motionHintPresenter, storeDailyTaskPresenter);
        states[typeof(ExitState_DailyTaskGame)] = new ExitState_DailyTaskGame(this, sceneRoot, timerPresenter, storeDailyTaskPresenter);
        states[typeof(WinState_DailyTaskGame)] = new WinState_DailyTaskGame(this, sceneRoot, timerPresenter);
    }

    public void Initialize()
    {
        SetState(GetState<StartState_DailyTaskGame>());
    }

    public void Dispose()
    {

    }

    public void SetState(IState state)
    {
        currentState?.ExitState();

        currentState = state;
        currentState.EnterState();
    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }
}
