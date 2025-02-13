using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPresenter
{
    private BotModel botModel;
    private BotView botView;
    private BotStateMachine botStateMachine;

    public BotPresenter(BotModel botModel, BotView botView, BotStateMachine botStateMachine)
    {
        this.botModel = botModel;
        this.botView = botView;
        this.botStateMachine = botStateMachine;
    }

    public void Initialize()
    {
        ActivateEvents();

        botStateMachine.Initialize();

        botModel.Initialize();
        botView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        botStateMachine.Dispose();

        botModel?.Dispose();
        botView?.Dispose();
    }

    private void ActivateEvents()
    {

    }

    private void DeactivateEvents()
    {

    }
}
