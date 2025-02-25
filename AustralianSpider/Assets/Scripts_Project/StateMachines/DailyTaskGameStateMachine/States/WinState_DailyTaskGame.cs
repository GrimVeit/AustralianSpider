using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState_DailyTaskGame : IState
{
    private UIDailyTaskGameSceneRoot sceneRoot;
    private TimerPresenter timerPresenter;

    private IGlobalStateMachine stateMachine;

    public WinState_DailyTaskGame(IGlobalStateMachine stateMachine, UIDailyTaskGameSceneRoot sceneRoot, TimerPresenter timerPresenter)
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
