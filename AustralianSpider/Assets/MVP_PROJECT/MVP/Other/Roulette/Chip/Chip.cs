using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Chip : MonoBehaviour
{
    public ChipData ChipData => chipData;
    public event Action<Chip> OnRetracted;
    public event Action<Chip> OnNoneRetracted;
    public event Action<Chip> OnFalled;

    [SerializeField] private Image image;
    private ICell cell;
    private ChipData chipData;

    private bool isRetract = false;

    public void Initialize(ChipData chipData, ICell cell)
    {
        this.chipData = chipData;
        this.cell = cell;

        image.sprite = this.chipData.Sprite;
        this.cell?.ChooseBet(this);
    }

    public void Retract()
    {
        if (isRetract) return;

        isRetract = true;
        cell?.ResetBet(this);
        transform.DOLocalMove(Vector2.zero, 0.7f).OnComplete(() => OnRetracted?.Invoke(this));
    }

    public void NoneRetract()
    {
        transform.DOLocalMove(Vector2.zero, 0.7f).OnComplete(() => OnNoneRetracted?.Invoke(this));
    }

    public void Fall(Vector2 vector)
    {
        transform.DOMove(vector, 0.7f).OnComplete(() => OnFalled?.Invoke(this));
    }
}
