using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Cards")]
public class Cards : ScriptableObject
{
    public List<CardInfo> cards = new List<CardInfo>();

    public CardInfo GetRandomCardInfo(TypeCard typeCard)
    {
        var cardInfos = cards.Where(data => data.cardType == typeCard).ToList();

        Debug.Log(typeCard + "//" + cardInfos.Count);

        return cardInfos[Random.Range(0, cardInfos.Count)];
    }
}

[System.Serializable]
public class CardInfo
{
    public int Number;
    public int PageNumber;
    public TypeCard cardType;
    public Sprite Sprite;
}
