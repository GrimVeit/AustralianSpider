using System;
using UnityEngine;

public class PackSpinModel
{
    public event Action OnAvailableBonusButton;
    public event Action OnUnvailableBonusButton;

    public event Action<Pack> OnGetPack_DataPack;
    public event Action<ShopItemPack> OnGetPack_Pack;
    public event Action OnActivateSpin;

    private bool isActive = true;

    private ISoundProvider soundProvider;
    private ISound spinSound;
    private IParticleEffectProvider particleEffectProvider;

    public PackSpinModel(ISoundProvider soundProvider, IParticleEffectProvider particleEffectProvider)
    {
        this.soundProvider = soundProvider;
        this.particleEffectProvider = particleEffectProvider;

        spinSound = this.soundProvider.GetSound("Wheel");
    }

    public void Spin()
    {
        if (isActive)
        {
            OnActivateSpin?.Invoke();
            spinSound.SetVolume(0.8f);
            spinSound.SetPitch(1);
            spinSound.Play();
        }
    }

    public void SetUnvailable()
    {
        isActive = false;
        OnUnvailableBonusButton?.Invoke();
    }

    public void SetAvailable()
    {
        isActive = true;
        OnAvailableBonusButton?.Invoke();
    }

    public void OnSpin(float speed)
    {
        //if (speed > 0.8f)
        //{
        //    return;
        //}

        //spinSound.SetVolume(speed / 2);

        //float pitch = Mathf.Lerp(1, 0.88f, 1 - speed);
        //spinSound.SetPitch(pitch * 1f);
    }

    public void GetShopItemPack(ShopItemPack shopItemPack)
    {
        //particleEffectProvider.Play("DailyBonus");
        //soundProvider.PlayOneShot("DailyBonus");
        OnGetPack_DataPack?.Invoke(shopItemPack.Pack);
        OnGetPack_Pack?.Invoke(shopItemPack);
    }
}
