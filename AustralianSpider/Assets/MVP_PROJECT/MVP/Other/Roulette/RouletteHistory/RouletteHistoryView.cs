using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteHistoryView : View
{
    [SerializeField] private Transform transformStartSpawn;
    [SerializeField] private Transform transformEndSpawn;

    [SerializeField] private RouletteNumberHistoryView rouletteNumberHistoryViewPrefab;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform canvas;
    [SerializeField] private Transform content;

    private Tween moveTween;
    private IEnumerator scrollIEnumerator;

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void AddRouletteNumberHistory(RouletteNumber rouletteNumber)
    {
        ScrollLeft();

        RouletteNumberHistoryView rouletteNumberHistoryView = Instantiate(rouletteNumberHistoryViewPrefab, canvas);
        rouletteNumberHistoryView.SetData(rouletteNumber);
        rouletteNumberHistoryView.transform.SetPositionAndRotation(transformStartSpawn.position, rouletteNumberHistoryViewPrefab.transform.rotation);

        rouletteNumberHistoryView.transform.DOMove(transformEndSpawn.position, 0.5f).OnComplete(() => AddToScroll(rouletteNumberHistoryView));
        rouletteNumberHistoryView.transform.DOScale(Vector3.one, 0.5f);

    }

    private void AddToScroll(RouletteNumberHistoryView rouletteNumberHistoryView)
    {
        rouletteNumberHistoryView.transform.SetParent(content);
        rouletteNumberHistoryView.transform.SetSiblingIndex(0);
    }

    public void Clear()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    private IEnumerator SmoothScrollRect(float targetPosition, float duration)
    {
        float startPosition = scrollRect.horizontalNormalizedPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        scrollRect.horizontalNormalizedPosition = targetPosition;
    }

    public void ScrollLeft()
    {
        if (scrollIEnumerator != null)
            Coroutines.Stop(scrollIEnumerator);

        scrollIEnumerator = SmoothScrollRect(0, 0.3f);
        Coroutines.Start(scrollIEnumerator);
    }

    public void SctrollRight()
    {
        if (scrollIEnumerator != null)
            Coroutines.Stop(scrollIEnumerator);

        scrollIEnumerator = SmoothScrollRect(1, 0.3f);
        Coroutines.Start(scrollIEnumerator);
    }
}
