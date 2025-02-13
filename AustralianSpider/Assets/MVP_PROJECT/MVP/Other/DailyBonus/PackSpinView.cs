using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PackSpinView : View
{
    [SerializeField] private List<ShopItemPack> shopItemsPacks = new List<ShopItemPack>();
    public event Action<float> OnSpin;
    public event Action OnEndSpin;
    public event Action OnClickSpinButton;
    public event Action<ShopItemPack> OnGetShopItemPack;

    //[SerializeField] private TextMeshProUGUI textCoins;
    //[SerializeField] private Button buttonDailyBonus;
    //[SerializeField] private Image buttonDailyBonusImage;
    //[SerializeField] private Sprite dailyBonusAvailableSprite;
    //[SerializeField] private Sprite dailyBonusUnvailableSprite;
    [SerializeField] private Vector3 spinVector;
    [SerializeField] private Transform spinTransform;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float minSpinSpeed;
    [SerializeField] private float maxSpinSpeed;
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;

    private IEnumerator rotateSpin_Coroutine;

    public void Initialize()
    {
        //buttonDailyBonus.onClick.AddListener(HandlerClickToSpinButton);
    }

    public void Dispose()
    {
        //buttonDailyBonus.onClick.RemoveListener(HandlerClickToSpinButton);
    }

    public void DeactivateSpinButton()
    {
        //buttonDailyBonusImage.sprite = dailyBonusUnvailableSprite;
    }

    public void ActivateSpinButton()
    {
        //buttonDailyBonusImage.sprite = dailyBonusAvailableSprite;
    }


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
            OnSpin?.Invoke(currentSpeed);

            //scrollRect.verticalNormalizedPosition += currentSpeed * Time.deltaTime;
            //scrollRect.verticalNormalizedPosition = scrollRect.verticalNormalizedPosition % 1; // Çàöèêëèâàíèå

            spinTransform.Rotate(spinVector * currentSpeed * Time.deltaTime);

            yield return null;
        }

        ShopItemPack shopItemPack = GetClosestBonus();
        Debug.Log(shopItemPack);
        OnGetShopItemPack?.Invoke(shopItemPack);
        OnEndSpin?.Invoke();
    }

    private ShopItemPack GetClosestBonus()
    {
        float minDistance = float.MaxValue;
        ShopItemPack closestBonus = null;


        foreach (var bonus in shopItemsPacks)
        {
            float distance = Vector2.Distance(bonus.Transform.position, centerPoint.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestBonus = bonus;
            }
        }


        return closestBonus;
    }

    private void HandlerClickToSpinButton()
    {
        OnClickSpinButton?.Invoke();
    }

}
