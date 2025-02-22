using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;
    private TimerPresenter timerPresenter;

    private IGlobalStateMachine stateMachine;

    public WinState_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, TimerPresenter timerPresenter)
    {
        this.sceneRoot = sceneRoot;
        this.stateMachine = stateMachine;
        this.timerPresenter = timerPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - WIN");

        sceneRoot.CloseHeaderPanel();
        sceneRoot.OpenWinPanel();
        timerPresenter.DeactivateTimer();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - WIN");
    }
}
