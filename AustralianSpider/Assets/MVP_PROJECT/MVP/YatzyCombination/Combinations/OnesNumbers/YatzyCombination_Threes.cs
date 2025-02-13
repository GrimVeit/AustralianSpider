using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YatzyCombination_Threes : YatzyCombination
{
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] protected private Button buttonChooseCombination;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite spriteSelect;
    [SerializeField] private Sprite spriteUnselect;

    private YatzyCombinationData yatzyCombinationData;

    public override void Initialize(YatzyCombinationData yatzyCombinationData)
    {
        Debug.Log(numbersCombinations.Count);

        this.yatzyCombinationData = yatzyCombinationData;
        List<int[]> firstList = new List<int[]>();
        foreach (var combination in numbersCombinations)
        {
            firstList.Add(combination.Numbers);
        }
        this.yatzyCombinationData.SetCombinations(firstList);
        this.yatzyCombinationData.SetIsOnlyNumbers(true);

        buttonChooseCombination.onClick.AddListener(HandlerClickToChooseCombinationButton);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonChooseCombination.onClick.RemoveListener(HandlerClickToChooseCombinationButton);
    }

    public override void CalculateScore(int[] diceValues)
    {
        int result = diceValues.Count(d => d == 3) * 3;

        textScore.text = result.ToString();

        yatzyCombinationData.SetScore(result);
    }

    public override void Select()
    {
        yatzyCombinationData.SetSelect(true);
        buttonImage.sprite = spriteSelect;
    }

    public override void Unselect()
    {
        yatzyCombinationData.SetSelect(false);
        buttonImage.sprite = spriteUnselect;
    }

    #region Input

    private void HandlerClickToChooseCombinationButton()
    {
        OnChooseCombination?.Invoke(yatzyCombinationData);
    }

    public override void Freeze()
    {
        buttonChooseCombination.enabled = false;
        buttonImage.sprite = spriteSelect;

        yatzyCombinationData.SetFreeze(true);
    }

    #endregion
}
