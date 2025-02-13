using System;
using System.Collections.Generic;

public class DiceRollPresenter : IDiceRollProvider
{
    private DiceRollModel diceRollModel;
    private DiceRollView diceRollView;

    public DiceRollPresenter(DiceRollModel diceRollModel, DiceRollView diceRollView)
    {
        this.diceRollModel = diceRollModel;
        this.diceRollView = diceRollView;
    }

    public void Initialize()
    {
        ActivateEvents();

        diceRollModel.Initialize();
        diceRollView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        diceRollModel.Dispose();
        diceRollView.Dispose();
    }

    private void ActivateEvents()
    {
        diceRollView.OnClickToRollButton += diceRollModel.StartRoll;
        diceRollView.OnStoppedDice_Action += diceRollModel.StopRoll;
        diceRollView.OnClickToDice += diceRollModel.FreezeToggle;

        diceRollModel.OnStartRoll_Indexes += diceRollView.Roll;
        diceRollModel.OnChangedAttemptsCount += diceRollView.OnChangeAttempts;

        diceRollModel.OnGetFullAttempt += diceRollView.ActivateButton;
        diceRollModel.OnStartRoll += diceRollView.DeactivateButton;
        diceRollModel.OnActivateRoll += diceRollView.ActivateButton;
        diceRollModel.OnDeactivateRoll += diceRollView.DeactivateButton;

        diceRollModel.OnFreeseDice += diceRollView.FreezeDice;
        diceRollModel.OnUnfreeseDice += diceRollView.UnfreeseDice;
    }

    private void DeactivateEvents()
    {
        diceRollView.OnClickToRollButton -= diceRollModel.StartRoll;
        diceRollView.OnStoppedDice_Action -= diceRollModel.StopRoll;
        diceRollView.OnClickToDice -= diceRollModel.FreezeToggle;

        diceRollModel.OnStartRoll_Indexes -= diceRollView.Roll;
        diceRollModel.OnChangedAttemptsCount -= diceRollView.OnChangeAttempts;

        diceRollModel.OnGetFullAttempt -= diceRollView.ActivateButton;
        diceRollModel.OnStartRoll -= diceRollView.DeactivateButton;
        diceRollModel.OnActivateRoll -= diceRollView.ActivateButton;
        diceRollModel.OnDeactivateRoll -= diceRollView.DeactivateButton;

        diceRollModel.OnFreeseDice -= diceRollView.FreezeDice;
        diceRollModel.OnUnfreeseDice -= diceRollView.UnfreeseDice;
    }

    #region Input

    public event Action<int> OnFreezeDice_Index
    {
        add { diceRollModel.OnFreeseDice += value; }
        remove { diceRollModel.OnFreeseDice -= value; }
    }

    public event Action<int> OnUnfreezeDice_Index
    {
        add { diceRollModel.OnUnfreeseDice += value; }
        remove { diceRollModel.OnUnfreeseDice -= value; }
    }

    public event Action<int, int[]> OnGetAllDiceValues
    {
        add { diceRollModel.OnGetAllDiceValues += value; }
        remove { diceRollModel.OnGetAllDiceValues -= value; }
    }

    public event Action OnGetFullAttempt
    {
        add { diceRollModel.OnGetFullAttempt += value; }
        remove { diceRollModel.OnGetFullAttempt -= value; }
    }

    public event Action OnLoseFirstAttempt
    {
        add { diceRollModel.OnLoseFirstAttempt += value; }
        remove { diceRollModel.OnLoseFirstAttempt -= value; }
    }

    public event Action OnStartRoll
    {
        add { diceRollModel.OnStartRoll += value; }
        remove { diceRollModel.OnStartRoll -= value; }
    }

    public event Action OnStopRoll
    {
        add { diceRollModel.OnStopRoll += value; }
        remove { diceRollModel.OnStopRoll -= value; }
    }

    public Dictionary<int, DiceData> Dices() => diceRollModel.dices;

    public void Reload()
    {
        diceRollModel.Reload();
    }

    public void ActivateFreezeToggle()
    {
        diceRollModel.ActivateFreezeToggle();
    }

    public void FreezeDice(int index)
    {
        diceRollModel.FreezeToggle(index);
    }

    public void UnfreezeDice(int index)
    {
        diceRollModel.UnfreezeToggle(index);
    }

    public void UnfreezeAllDices()
    {
        diceRollModel.AllUnfreeze();
    }

    public void DeactivateFreezeToggle()
    {
        diceRollModel.DeactivateFreezeToggle();
    }

    public void StartRoll()
    {
        diceRollModel.StartRoll();
    }

    #endregion
}

public interface IDiceRollProvider
{
    public Dictionary<int, DiceData> Dices();
    public event Action<int, int[]> OnGetAllDiceValues;
    public event Action OnStartRoll;
    public event Action OnStopRoll;
    public void StartRoll();
    public void Reload();
    public void FreezeDice(int index);
    public void UnfreezeDice(int index);
    public void UnfreezeAllDices();
}
