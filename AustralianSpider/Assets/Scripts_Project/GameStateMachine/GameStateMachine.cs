using System;
using System.Collections.Generic;

public class GameStateMachine : IGlobalStateMachine
{
    private Dictionary<Type, IState> states = new Dictionary<Type, IState>();

    private IState currentState;

    public GameStateMachine(
        StoreGameDesignPresenter storeGameDesignPresenter,
        StoreCoverCardDesignPresenter storeCoverCardDesignPresenter,
        StoreFaceCardDesignPresenter storeFaceCardDesignPresenter,
        StoreGameTypePresenter storeGameTypePresenter,
        StoreCardPresenter storeCardPresenter,
        GameDesignPresenter gameDesignPresenter,
        CardColumnPresenter cardColumnPresenter,
        UIMiniGameSceneRoot sceneRoot,
        ScorePresenter scorePresenter,
        MotionCounterPresenter motionCounterPresenter,
        CardMotionHistoryPresenter cardMotionHistoryPresenter,
        TimerPresenter timerPresenter)
    {
        states[typeof(StartState_Game)] = new StartState_Game(this, storeGameDesignPresenter, storeCoverCardDesignPresenter, storeFaceCardDesignPresenter, storeGameTypePresenter, storeCardPresenter, gameDesignPresenter, cardColumnPresenter, timerPresenter);
        states[typeof(MainState_Game)] = new MainState_Game(this, sceneRoot, cardColumnPresenter, storeCardPresenter, scorePresenter, motionCounterPresenter, cardMotionHistoryPresenter, timerPresenter);
        states[typeof(ExitState_Game)] = new ExitState_Game(this, sceneRoot, timerPresenter);
        states[typeof(RestartState_Game)] = new RestartState_Game(this, sceneRoot, timerPresenter);
        states[typeof(WinState_Game)] = new WinState_Game(this, sceneRoot, timerPresenter);
    }

    public void Initialize()
    {
        SetState(GetState<StartState_Game>());
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

public interface IGlobalStateMachine
{
    public void SetState(IState state);

    public IState GetState<T>() where T : IState;
}
