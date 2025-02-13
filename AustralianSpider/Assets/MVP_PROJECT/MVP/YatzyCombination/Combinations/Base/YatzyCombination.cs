using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class YatzyCombination : MonoBehaviour
{
    public Action<YatzyCombinationData> OnChooseCombination;
    public List<YatzyNumbersCombination> numbersCombinations = new List<YatzyNumbersCombination>();

    public abstract void Initialize(YatzyCombinationData yatzyCombinationData);
    public virtual void Dispose() { }
    public abstract void CalculateScore(int[] diceValues);
    public abstract void Select();
    public abstract void Unselect();
    public abstract void Freeze();
}

[System.Serializable]
public class YatzyNumbersCombination
{
    public int[] Numbers;
}

public class ChooseYatzyCombination
{
    public int IndexCombination { get; private set; }

    public int[] NumberCombination { get; private set; }
    public Dictionary<int, bool> MatchDictionary { get; private set; }

    public void SetData(int index, int[] numbers, Dictionary<int, bool> matchDictionary)
    {
        this.IndexCombination = index;
        NumberCombination = numbers;
        this.MatchDictionary = matchDictionary;
    }
}
