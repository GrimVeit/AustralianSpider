using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMoveAIModel
{
    public event Action OnStartMove;
    public event Action OnEndMove;
    public event Action OnTeleporting;

    private ISoundProvider soundProvider;

    public CardMoveAIModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void EndMove()
    {
        soundProvider.PlayOneShot("CardDrop");
        OnEndMove?.Invoke();
    }

    public void Activate()
    {
        soundProvider.PlayOneShot("CardGrab");
        OnStartMove?.Invoke();
    }

    public void Deactivate()
    {
        OnTeleporting?.Invoke();
    }
}
