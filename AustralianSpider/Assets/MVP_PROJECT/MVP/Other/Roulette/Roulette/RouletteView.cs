using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouletteView : View
{
    [SerializeField] private Button spinButton;
    [SerializeField] private Vector3 spinVector;
    [SerializeField] private Transform spinTransform;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float minSpinSpeed;
    [SerializeField] private float maxSpinSpeed;
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;

    [SerializeField] private Transform ball;
    [SerializeField] private List<RouletteSlotValue> rouletteSlotValues = new List<RouletteSlotValue>();

    private IEnumerator rotateSpin_Coroutine;

    public void Initialize()
    {
        spinButton.onClick.AddListener(HandlerClickToSpinButton);
    }

    public void Dispose()
    {
        spinButton.onClick.RemoveListener(HandlerClickToSpinButton);
    }

    public void RollBallToSlot(Vector3 vector)
    {
        RouletteSlotValue rouletteSlotValue = GetClosestSlot(vector);
        ball.SetParent(rouletteSlotValue.transform);
        ball.SetLocalPositionAndRotation(rouletteSlotValue.StartTransform.localPosition, Quaternion.identity);
        ball.DOLocalMove(rouletteSlotValue.EndTransform.localPosition, 0.5f).OnComplete(() => 
        {
            OnGetRouletteNumber?.Invoke(rouletteSlotValue);
            Debug.Log("ּÿק ג סכמעו");
        });
    }

    private RouletteSlotValue GetClosestSlot(Vector3 vector)
    {
        return rouletteSlotValues.OrderBy(rv => Vector3.Distance(vector, rv.SlotTransform.position)).First();
    }

    #region Spin

    public void StartSpin()
    {
        if (rotateSpin_Coroutine != null)
            Coroutines.Stop(rotateSpin_Coroutine);

        rotateSpin_Coroutine = RotateSpin_Coroutine();
        Coroutines.Start(rotateSpin_Coroutine);
    }

    private IEnumerator RotateSpin_Coroutine()
    {
        float elapsedTime = 0f;
        float startSpeed = UnityEngine.Random.Range(minSpinSpeed, maxSpinSpeed);
        float duration = UnityEngine.Random.Range(minDuration, maxDuration);
        float endSpeed = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float currentSpeed = Mathf.Lerp(startSpeed, endSpeed, elapsedTime / duration);

            spinTransform.Rotate(spinVector * currentSpeed * Time.deltaTime);

            yield return null;
        }
    }

    #endregion

    #region Input

    public event Action<RouletteSlotValue> OnGetRouletteNumber;
    public event Action OnStartSpin;

    private void HandlerClickToSpinButton()
    {
        OnStartSpin?.Invoke();
    }

    #endregion
}
