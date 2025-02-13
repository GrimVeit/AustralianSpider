using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardView : View
{
    public event Action OnClickDailyReward;

    [SerializeField] private Button dailyRewardButton;

    private Vector3 normalScaleRewardButton;
    private Tween scaleTween;

    public void Initialize()
    {
        normalScaleRewardButton = dailyRewardButton.transform.localScale;

        scaleTween = dailyRewardButton.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.6f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);

        dailyRewardButton.onClick.AddListener(HandlerClickToDailyReward);
    }

    public void Dispose()
    {
        if (scaleTween != null)
            scaleTween.Kill();

        dailyRewardButton.onClick.RemoveListener(HandlerClickToDailyReward);
    }

    private void HandlerClickToDailyReward()
    {
        OnClickDailyReward?.Invoke();
    }
}
