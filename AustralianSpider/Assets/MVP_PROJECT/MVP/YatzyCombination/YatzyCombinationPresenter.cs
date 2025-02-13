using System;
using System.Collections.Generic;

public class YatzyCombinationPresenter : IYatzyCombinationsProvider
{
    private YatzyCombinationModel yatzyCombinationModel;
    private YatzyCombinationView yatzyCombinationView;

    public YatzyCombinationPresenter(YatzyCombinationModel yatzyCombinationModel, YatzyCombinationView yatzyCombinationView)
    {
        this.yatzyCombinationModel = yatzyCombinationModel;
        this.yatzyCombinationView = yatzyCombinationView;
    }

    public void Initialize()
    {
        ActivateEvents();

        yatzyCombinationModel.Initialize();
        yatzyCombinationView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        yatzyCombinationModel.Dispose();
        yatzyCombinationView.Dispose();
    }

    private void ActivateEvents()
    {
        yatzyCombinationModel.OnInitialize += yatzyCombinationView.InitializeYatzyCombinations;

        yatzyCombinationView.OnChooseCombination += yatzyCombinationModel.SelectCombinationForFreeze;
        yatzyCombinationView.OnClickToPlay += yatzyCombinationModel.SubmitChooseCombinationToFreeze;

        yatzyCombinationModel.OnSetNumbersCombination += yatzyCombinationView.SetNumbersCombination;

        yatzyCombinationModel.OnSelectCombination_Index += yatzyCombinationView.Select;
        yatzyCombinationModel.OnUnselectCombination_Index += yatzyCombinationView.Unselect;
        yatzyCombinationModel.OnFreezeCombination_Index += yatzyCombinationView.Freeze;

        yatzyCombinationModel.OnSelectCombination += yatzyCombinationView.ActivateButtonPlay;
        yatzyCombinationModel.OnUnselectCombination += yatzyCombinationView.DeactivateButtonPlay;
        yatzyCombinationModel.OnFreezeCombination += yatzyCombinationView.DeactivateButtonPlay;
    }

    private void DeactivateEvents()
    {

    }

    #region Input

    public void SetNumbersCombination(int attempt, int[] numbers)
    {
        yatzyCombinationModel.SetNumbersCombination(numbers);
    }

    public void Activate()
    {
        yatzyCombinationModel.Activate();
    }

    public void Deactivate()
    {
        yatzyCombinationModel.Deactivate();
    }

    public void FreezeBestCombination()
    {
        yatzyCombinationModel.FreezeBestCombinationForFreeze();
    }

    public void FreezeCombination(int index)
    {
        yatzyCombinationModel.SelectCombinationForFreeze(index);
    }

    public void SubmitFreezeCombination()
    {
        yatzyCombinationModel.SubmitChooseCombinationToFreeze();
    }

    public Dictionary<int, YatzyCombinationData> YatzyCombinations() => yatzyCombinationModel.YatzyCombinations;

    public event Action OnSelectCombination
    {
        add { yatzyCombinationModel.OnSelectCombination += value; }
        remove { yatzyCombinationModel.OnSelectCombination -= value; }
    }

    public event Action OnFreezeYatzyCombination
    {
        add { yatzyCombinationModel.OnFreezeCombination += value; }
        remove { yatzyCombinationModel.OnFreezeCombination -= value; }
    }

    public event Action<int> OnSelectCombination_Index
    {
        add { yatzyCombinationModel.OnSelectCombination_Index += value; }
        remove { yatzyCombinationModel.OnSelectCombination_Index -= value; }
    }

    public event Action<int> OnUnselectCombination_Index
    {
        add { yatzyCombinationModel.OnUnselectCombination_Index += value; }
        remove { yatzyCombinationModel.OnUnselectCombination_Index -= value; }
    }

    public event Action<int> OnFreezeCombination_Index
    {
        add { yatzyCombinationModel.OnFreezeCombination_Index += value; }
        remove { yatzyCombinationModel.OnFreezeCombination_Index -= value; }
    }

    public event Action OnFinishGame
    {
        add { yatzyCombinationModel.OnFinishGame += value; }
        remove { yatzyCombinationModel.OnFinishGame -= value; }
    }

    public event Action<int, bool> OnGetScore
    {
        add { yatzyCombinationModel.OnGetScore += value; }
        remove { yatzyCombinationModel.OnGetScore -= value; }
    }

    #endregion
}

public interface IYatzyCombinationsProvider
{
    public Dictionary<int, YatzyCombinationData> YatzyCombinations();

    public void FreezeCombination(int index);
    public void FreezeBestCombination();
    public void SubmitFreezeCombination();
}
