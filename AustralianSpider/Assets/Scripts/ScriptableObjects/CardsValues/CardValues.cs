using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CardValues : ScriptableObject
{
    public List<CardValue> CardsValues = new List<CardValue>();

    public CardValue GetRandom()
    {
        int index = Random.Range(0, CardsValues.Count);
        return CardsValues[index];
    }
}

[System.Serializable]
public class CardValue
{
    [SerializeField] private int idCard;
    [SerializeField] private int nominal;
    [SerializeField] private Sprite sprite;

    public int CardID => idCard;
    public int CardNominal => nominal;
    public Sprite CardSprite => sprite;
}
