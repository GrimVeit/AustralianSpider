using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Cards", menuName = "Cards/CardGroup")]
public class Cards : ScriptableObject
{
    public List<Card> cards = new List<Card>();

    public Card GetCardById(int id)
    {
        return cards.FirstOrDefault(data => data.CardId == id);
    }
}
