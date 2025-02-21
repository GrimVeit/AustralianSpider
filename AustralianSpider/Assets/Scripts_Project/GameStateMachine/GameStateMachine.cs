using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : IGlobalStateMachine
{
    private Dictionary<Type, IState> states = new Dictionary<Type, IState>();

    private IState currentState;

    public GameStateMachine()
    {
        states[typeof(StartState_Game)] = new StartState_Game();
        states[typeof(MainState_Game)] = new MainState_Game();
        states[typeof(ExitState_Game)] = new ExitState_Game();
        states[typeof(RestartState_Game)] = new RestartState_Game();
        states[typeof(WinState_Game)] = new WinState_Game();
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
