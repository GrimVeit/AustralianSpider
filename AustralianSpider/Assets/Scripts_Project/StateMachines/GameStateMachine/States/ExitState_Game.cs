using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitState_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;
    private TimerPresenter timerPresenter;

    private IGlobalStateMachine stateMachine;

    public ExitState_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, TimerPresenter timerPresenter)
    {
        this.sceneRoot = sceneRoot;
        this.stateMachine = stateMachine;
        this.timerPresenter = timerPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - EXIT");

        sceneRoot.OnClickToCancel_Exit += ChangeStateToMain;

        sceneRoot.CloseHeaderPanel();
        sceneRoot.OpenExitPanel();
        timerPresenter.PauseTimer();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - EXIT");

        sceneRoot.OnClickToCancel_Exit -= ChangeStateToMain;

        sceneRoot.CloseExitPanel();
    }

    private void ChangeStateToMain()
    {
        stateMachine.SetState(stateMachine.GetState<MainState_Game>());
    }
}
