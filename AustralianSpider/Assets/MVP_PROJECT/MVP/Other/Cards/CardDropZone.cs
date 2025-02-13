using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDropZone : MonoBehaviour
{
    public Owner Owner => owner;

    [SerializeField] private Owner owner;
    public event Action OnSpawnCard;

    public void SpawnCard()
    {
        OnSpawnCard?.Invoke();
    }
}

public enum Owner
{
    User, AI
}
