using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartState_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;
    private TimerPresenter timerPresenter;

    private IGlobalStateMachine stateMachine;

    public RestartState_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, TimerPresenter timerPresenter)
    {
        this.sceneRoot = sceneRoot;
        this.stateMachine = stateMachine;
        this.timerPresenter = timerPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - RESTART");

        sceneRoot.OnClickToCancel_Restart += ChangeStateToMain;

        sceneRoot.CloseHeaderPanel();
        sceneRoot.OpenRestartPanel();
        timerPresenter.PauseTimer();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - RESTART");

        sceneRoot.OnClickToCancel_Restart -= ChangeStateToMain;

        sceneRoot.CloseRestartPanel();
    }

    private void ChangeStateToMain()
    {
        stateMachine.SetState(stateMachine.GetState<MainState_Game>());
    }
}
