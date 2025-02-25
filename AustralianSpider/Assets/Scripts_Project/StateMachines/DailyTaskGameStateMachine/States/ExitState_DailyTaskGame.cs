using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitState_DailyTaskGame : IState
{
    private UIDailyTaskGameSceneRoot sceneRoot;
    private StoreDailyTaskPresenter storeDailyTaskPresenter;
    private TimerPresenter timerPresenter;

    private IGlobalStateMachine stateMachine;

    public ExitState_DailyTaskGame(IGlobalStateMachine stateMachine, UIDailyTaskGameSceneRoot sceneRoot, TimerPresenter timerPresenter, StoreDailyTaskPresenter storeDailyTaskPresenter)
    {
        this.sceneRoot = sceneRoot;
        this.stateMachine = stateMachine;
        this.timerPresenter = timerPresenter;
        this.storeDailyTaskPresenter = storeDailyTaskPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - EXIT");

        sceneRoot.OnClickToCancel_Exit += ChangeStateToMain;
        sceneRoot.OnClickToExit += storeDailyTaskPresenter.SetLoseStatus;

        sceneRoot.CloseHeaderPanel();
        sceneRoot.OpenExitPanel();
        timerPresenter.PauseTimer();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - EXIT");

        sceneRoot.OnClickToCancel_Exit -= ChangeStateToMain;
        sceneRoot.OnClickToExit -= storeDailyTaskPresenter.SetLoseStatus;

        sceneRoot.CloseExitPanel();
    }

    private void ChangeStateToMain()
    {
        stateMachine.SetState(stateMachine.GetState<MainState_DailyTaskGame>());
    }
}
