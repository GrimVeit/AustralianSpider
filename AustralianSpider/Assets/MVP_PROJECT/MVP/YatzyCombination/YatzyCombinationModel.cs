using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class YatzyCombinationModel
{
    public event Action<int, YatzyCombinationData> OnInitialize;
    public event Action OnFinishGame;
    public event Action<int, bool> OnGetScore;

    public event Action<int[], int[]> OnSetNumbersCombination;

    public event Action<int> OnSelectCombination_Index;
    public event Action<int> OnUnselectCombination_Index; 
    public event Action<int> OnFreezeCombination_Index;

    public event Action OnSelectCombination;
    public event Action OnUnselectCombination;
    public event Action OnFreezeCombination;

    public Dictionary<int, YatzyCombinationData> YatzyCombinations = new Dictionary<int, YatzyCombinationData>();

    private int maxCountCombinations;
    private int currentCountFreezeCombinations = 0;

    private ISoundProvider soundProvider;


    private YatzyCombinationData currentSelectYatzyCombination;

    private bool isActive = false;

    public YatzyCombinationModel(int maxCountCombinations, ISoundProvider soundProvider)
    {
        this.maxCountCombinations = maxCountCombinations;
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        for (int i = 0; i < maxCountCombinations; i++)
        {
            YatzyCombinationData yatzyCombinationData = new YatzyCombinationData(i);
            YatzyCombinations[i] = yatzyCombinationData;
            OnInitialize?.Invoke(i, yatzyCombinationData);
        }
    }

    public void Dispose()
    {

    }

    public void SetNumbersCombination(int[] numbers)
    {
        for (int i = 0; i < YatzyCombinations.Count; i++)
        {
            Debug.Log("Комбинация - " + YatzyCombinations[i] + ", количество вариантов - " + YatzyCombinations[i].NumbersCombinations.Count);
        }

        OnSetNumbersCombination?.Invoke(YatzyCombinations.
            Where(d => d.Value.IsFreeze == false)
            .Select(d => d.Key)
            .ToArray(),  numbers);
    }

    public void FreezeBestCombinationForFreeze()
    {
        if (currentSelectYatzyCombination != null)
        {
            OnUnselectCombination?.Invoke();
            OnUnselectCombination_Index?.Invoke(currentSelectYatzyCombination.Index);
            currentSelectYatzyCombination = null;
        }

        currentSelectYatzyCombination = YatzyCombinations.Where(pair => !pair.Value.IsFreeze).OrderByDescending(pair => pair.Value.Score).FirstOrDefault().Value;
        OnSelectCombination_Index?.Invoke(currentSelectYatzyCombination.Index);
        OnSelectCombination?.Invoke();
    }

    public void SelectCombinationForFreeze(YatzyCombinationData yatzyCombination)
    {
        if (!isActive) return;

        soundProvider.PlayOneShot("SelectCombination");

        if (YatzyCombinations[yatzyCombination.Index].IsFreeze) return;

        if (yatzyCombination == currentSelectYatzyCombination)
        {
            OnUnselectCombination?.Invoke();
            OnUnselectCombination_Index?.Invoke(currentSelectYatzyCombination.Index);
            currentSelectYatzyCombination = null;
        }
        else
        {
            if (currentSelectYatzyCombination != null)
            {
                OnUnselectCombination?.Invoke();
                OnUnselectCombination_Index?.Invoke(currentSelectYatzyCombination.Index);
            }

            currentSelectYatzyCombination = yatzyCombination;
            OnSelectCombination?.Invoke();
            OnSelectCombination_Index?.Invoke(currentSelectYatzyCombination.Index);
        }

    }

    public void SelectCombinationForFreeze(int index)
    {
        if (!isActive) return;

        if (YatzyCombinations[index].IsFreeze) return;

        soundProvider.PlayOneShot("SelectCombination");

        if (YatzyCombinations[index] == currentSelectYatzyCombination)
        {
            OnUnselectCombination?.Invoke();
            OnUnselectCombination_Index?.Invoke(currentSelectYatzyCombination.Index);
            currentSelectYatzyCombination = null;
        }
        else
        {
            if (currentSelectYatzyCombination != null)
            {
                OnUnselectCombination?.Invoke();
                OnUnselectCombination_Index?.Invoke(currentSelectYatzyCombination.Index);
            }

            currentSelectYatzyCombination = YatzyCombinations[index];
            OnSelectCombination?.Invoke();
            OnSelectCombination_Index?.Invoke(currentSelectYatzyCombination.Index);
        }

    }

    private void UnselectCombination()
    {
        if (currentSelectYatzyCombination != null)
        {
            OnUnselectCombination?.Invoke();
            OnUnselectCombination_Index?.Invoke(currentSelectYatzyCombination.Index);
            currentSelectYatzyCombination = null;
        }
    }

    public void SubmitChooseCombinationToFreeze()
    {
        soundProvider.PlayOneShot("ClickEnter");

        currentCountFreezeCombinations += 1;
        YatzyCombinations[currentSelectYatzyCombination.Index] = currentSelectYatzyCombination;
        OnGetScore?.Invoke(currentSelectYatzyCombination.Score, currentSelectYatzyCombination.IsNumbersOnly);
        OnFreezeCombination_Index?.Invoke(currentSelectYatzyCombination.Index);
        currentSelectYatzyCombination = null;
        OnFreezeCombination?.Invoke();

        if(currentCountFreezeCombinations >= maxCountCombinations)
        {
            OnFinishGame?.Invoke();
        }
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        UnselectCombination();
        isActive = false;
    }
}
