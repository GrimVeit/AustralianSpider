using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;

    private IGlobalStateMachine stateMachine;

    public MainState_Game(IGlobalStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void EnterState()
    {
        
    }

    public void ExitState()
    {

    }

    private void ChangeStateToRestart()
    {
        stateMachine.SetState(stateMachine.GetState<RestartState_Game>());
    }

    private void ChangeStateToExit()
    {
        stateMachine.SetState(stateMachine.GetState<ExitState_Game>());
    }

    private void ChangeStateToWin()
    {
        stateMachine.SetState(stateMachine.GetState<WinState_Game>());
    }
}
