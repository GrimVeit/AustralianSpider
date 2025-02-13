using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YatzyCombinationView : View, IIdentify
{
    public string GetID() => idComponent;

    [SerializeField] private string idComponent;

    public event Action OnClickToPlay;
    public event Action<YatzyCombinationData> OnChooseCombination;

    [SerializeField] private List<YatzyCombination> yatzyCombinations = new List<YatzyCombination>();
    [SerializeField] private YatzyCombinationButton yatzyCombinationButton;

    public void Initialize()
    {
        yatzyCombinationButton.OnClickToPlayButton += HandleClickToPlay;
        yatzyCombinationButton.Initialize();
    }

    public void InitializeYatzyCombinations(int index, YatzyCombinationData yatzyCombinationData)
    {
        yatzyCombinations[index].OnChooseCombination += HandleChooseCombination;
        yatzyCombinations[index].Initialize(yatzyCombinationData);
    }

    public void Dispose()
    {
        for (int i = 0; i < yatzyCombinations.Count; i++)
        {
            yatzyCombinations[i].OnChooseCombination -= HandleChooseCombination;
            yatzyCombinations[i].Dispose();
        }

        yatzyCombinationButton.OnClickToPlayButton -= HandleClickToPlay;
        yatzyCombinationButton.Dispose();
    } 

    public void SetNumbersCombination(int[] indexesCombinations, int[] numbers)
    {
        for (int i = 0; i < indexesCombinations.Length; i++)
        {
            yatzyCombinations[indexesCombinations[i]].CalculateScore(numbers);
        }
    }

    public void Select(int index)
    {
        yatzyCombinations[index].Select();
    }

    public void Unselect(int index)
    {
        yatzyCombinations[index].Unselect();
    }

    public void Freeze(int index)
    {
        yatzyCombinations[index].Freeze();
    }


    public void ActivateButtonPlay()
    {
        yatzyCombinationButton.ActivateButton();
    }

    public void DeactivateButtonPlay()
    {
        yatzyCombinationButton.DeactivateButton();
    }

    #region Input

    private void HandleChooseCombination(YatzyCombinationData yatzyCombinationData)
    {
        OnChooseCombination?.Invoke(yatzyCombinationData);
    }

    private void HandleClickToPlay()
    {
        OnClickToPlay?.Invoke();
    }

    #endregion
}
