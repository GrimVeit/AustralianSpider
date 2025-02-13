using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RouletteBallView : View
{
    public event Action<Vector3> OnBallStopped;
    public event Action OnClickToSpinButton;

    [SerializeField] private Transform transformParent;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private Transform ball;
    [SerializeField] private Transform transformStart;
    [SerializeField] private Transform transformEnd;
    private float startRadius;
    private float endRadius;
    [SerializeField] private float duration;
    [SerializeField] private float startSpeed;
    [SerializeField] private float endSpeed = 0;

    [SerializeField] private Button spinButton;

    private float currentRadius;
    private float currentSpeed;
    private float angle;

    public void Initialize()
    {
        spinButton.onClick.AddListener(HandlerClickSpinButton);

        startRadius = Vector3.Distance(transformStart.position, centerPoint.position);
        endRadius = Vector3.Distance(transformEnd.position, centerPoint.position);
    }

    public void Dispose()
    {
        spinButton.onClick.RemoveListener(HandlerClickSpinButton);
    }

    public void StartSpin()
    {
        Coroutines.Start(MoveBall());
        DOTween.To(() => currentRadius, x => currentRadius = x, endRadius, duration);
        DOTween.To(() => currentSpeed, x => currentSpeed = x, endSpeed, duration);
    }

    private IEnumerator MoveBall()
    {
        currentSpeed = startSpeed;
        currentRadius = startRadius;
        angle = 0f;

        ball.transform.SetParent(transformParent);

        while(currentRadius > endRadius)
        {
            angle += currentSpeed * Time.deltaTime;

            float x = centerPoint.position.x + Mathf.Cos(angle) * currentRadius;
            float y = centerPoint.position.y + Mathf.Sin(angle) * currentRadius;

            ball.transform.position = new Vector3(x, y, ball.transform.position.z);

            yield return null;
        }

        OnBallStopped?.Invoke(ball.transform.position);
    }

    #region Input

    private void HandlerClickSpinButton()
    {
        OnClickToSpinButton?.Invoke();
    }

    #endregion
}
