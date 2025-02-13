using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownView : View, IIdentify
{
    [SerializeField] private string viewID;

    public event Action OnClickCooldownButton;

    [SerializeField] private Button cooldownButton;
    [SerializeField] private GameObject cooldownObject;
    [SerializeField] private TextMeshProUGUI textCountdown;

    private Vector3 normalScaleCooldownButton;
    private Tween scaleTween;

    public string GetID() => viewID;

    public void Initialize()
    {
        cooldownButton.onClick.AddListener(HandlerClickToCooldownButton);

        normalScaleCooldownButton = cooldownButton.transform.localScale;
    }

    public void Dispose()
    {
        cooldownButton.onClick.RemoveListener(HandlerClickToCooldownButton);
    }

    public void ChangeTimer(string time)
    {
        textCountdown.text = time;
    }

    public void OnActivateButton()
    {
        cooldownObject.SetActive(false);

        scaleTween = cooldownButton.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.6f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }

    public void OnDeactivateButton()
    {
        cooldownObject.SetActive(true);

        if (scaleTween != null)
            scaleTween.Kill();
        cooldownButton.transform.localScale = normalScaleCooldownButton;
    }

    private void HandlerClickToCooldownButton()
    {
        OnClickCooldownButton?.Invoke();
    }
}
