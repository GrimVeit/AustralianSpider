using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreView : View
{
    [Header("Score")]
    [SerializeField] private List<TextMeshProUGUI> textCoins = new List<TextMeshProUGUI>();

    [Header("Health")]
    [SerializeField] private Transform parentEggsHealth;
    [SerializeField] private HealthStar healthPrefab;
    [SerializeField] private Sprite spriteActiveHealth;
    [SerializeField] private Sprite spriteInactiveHealth;

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Score

    public void DisplayCoins(int coins)
    {
        textCoins.ForEach(x => x.text = (coins.ToString() + " coins") );
    }

    #endregion

    #region Health

    public void AddHealth(int countValue)
    {
        for (int i = 0; i < countValue; i++)
        {
            Instantiate(healthPrefab, parentEggsHealth);
        }
    }

    public void RemoveHealth()
    {
        Destroy(parentEggsHealth.GetChild(parentEggsHealth.childCount - 1).gameObject);
    }

    #endregion
}
