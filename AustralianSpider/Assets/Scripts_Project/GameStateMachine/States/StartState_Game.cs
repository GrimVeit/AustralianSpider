using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState_Game : IState
{
    private StoreGameDesignPresenter storeGameDesignPresenter;
    private StoreCoverCardDesignPresenter storeCoverCardDesignPresenter;
    private StoreFaceCardDesignPresenter storeFaceCardDesignPresenter;
    private StoreGameTypePresenter storeGameTypePresenter;
    private StoreCardPresenter storeCardPresenter;
    private GameDesignPresenter gameDesignPresenter;
    private CardColumnPresenter cardColumnPresenter;

    private IGlobalStateMachine stateMachine;

    public StartState_Game(
        IGlobalStateMachine stateMachine, 
        StoreGameDesignPresenter storeGameDesignPresenter, 
        StoreCoverCardDesignPresenter storeCoverCardDesignPresenter, 
        StoreFaceCardDesignPresenter storeFaceCardDesignPresenter, 
        StoreGameTypePresenter storeGameTypePresenter, 
        StoreCardPresenter storeCardPresenter, 
        GameDesignPresenter gameDesignPresenter,
        CardColumnPresenter cardColumnPresenter)
    {
        this.stateMachine = stateMachine;
        this.storeGameDesignPresenter = storeGameDesignPresenter;
        this.storeCoverCardDesignPresenter = storeCoverCardDesignPresenter;
        this.storeFaceCardDesignPresenter = storeFaceCardDesignPresenter;
        this.storeGameTypePresenter = storeGameTypePresenter;
        this.storeCardPresenter = storeCardPresenter;
        this.gameDesignPresenter = gameDesignPresenter;
        this.cardColumnPresenter = cardColumnPresenter;
    }

    public void EnterState()
    {
        storeGameDesignPresenter.OnSelectGameDesign += gameDesignPresenter.SetGameDesign;
        storeCoverCardDesignPresenter.OnSelectCoverCardDesign += storeCardPresenter.SetCoverCardDesign;
        storeFaceCardDesignPresenter.OnSelectFaceCardDesign += storeCardPresenter.SetFaceCardDesign;
        storeGameTypePresenter.OnSelectGameType += storeCardPresenter.SetGameType;
        storeCardPresenter.OnDealCards_Value += cardColumnPresenter.DealCards;

        storeCardPresenter.CreateCards();
        storeCardPresenter.DealCards();

        ChangeStateToMain();
    }

    public void ExitState()
    {
        storeGameDesignPresenter.OnSelectGameDesign -= gameDesignPresenter.SetGameDesign;
        storeCoverCardDesignPresenter.OnSelectCoverCardDesign -= storeCardPresenter.SetCoverCardDesign;
        storeFaceCardDesignPresenter.OnSelectFaceCardDesign -= storeCardPresenter.SetFaceCardDesign;
        storeGameTypePresenter.OnSelectGameType -= storeCardPresenter.SetGameType;
        storeCardPresenter.OnDealCards_Value -= cardColumnPresenter.DealCards;
    }

    private void ChangeStateToMain()
    {
        stateMachine.SetState(stateMachine.GetState<MainState_Game>());
    }
}
