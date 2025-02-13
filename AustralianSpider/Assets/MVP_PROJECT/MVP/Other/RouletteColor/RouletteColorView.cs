using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RouletteColorView : View
{
    public event Action<int> OnChooseColorIndex;

    [SerializeField] private List<RouletteColor> rouletteColorList = new List<RouletteColor>();
    [SerializeField] private TextMeshProUGUI textNameCurrentDesign;

    private int currentIndex;

    public void Initialize()
    {
        for (int i = 0; i < rouletteColorList.Count; i++)
        {
            rouletteColorList[i].OnChooseColorIndex += HandlerToChooseColorIndex;
            rouletteColorList[i].Initialize(i);
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < rouletteColorList.Count; i++)
        {
            rouletteColorList[i].OnChooseColorIndex -= HandlerToChooseColorIndex;
            rouletteColorList[i].Dispose();
        }
    }

    public void ChooseColorIndex(int index)
    {
        if (!rouletteColorList[currentIndex].IsActiveButton)
        {
            rouletteColorList[currentIndex].ActivateButton();
        }

        currentIndex = index;
        textNameCurrentDesign.text = rouletteColorList[index].NameDesign;
        rouletteColorList[currentIndex].DeactivateButton();
    }

    #region Input

    private void HandlerToChooseColorIndex(int index)
    {
        OnChooseColorIndex?.Invoke(index);
    }

    #endregion
}
