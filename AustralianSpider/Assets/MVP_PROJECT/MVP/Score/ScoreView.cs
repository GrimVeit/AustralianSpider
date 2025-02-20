using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreView : View
{
    [Header("Score")]
    [SerializeField] private List<TextMeshProUGUI> textCoins = new List<TextMeshProUGUI>();

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Score

    public void DisplayCoins(int coins)
    {
        textCoins.ForEach(x => x.text = coins.ToString());
    }

    #endregion

}
