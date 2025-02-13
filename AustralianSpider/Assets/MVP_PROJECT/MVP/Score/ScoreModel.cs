using System;
using UnityEngine;

public class ScoreModel
{
    public event Action OnGameWinned;
    public event Action OnGameFailed;


    public event Action OnRemoveHealth;
    public event Action<int> OnAddHealth;

    public event Action<int> OnChangeAllCountCoins;
    public event Action<int> OnGetCoins;

    private int currentRecord;

    private int currentHealth;

    private IMoneyProvider moneyProvider;
    private ISoundProvider soundProvider;
     
    public ScoreModel(IMoneyProvider moneyProvider, ISoundProvider soundProvider)
    {
        this.moneyProvider = moneyProvider;
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        currentHealth = 5;
        currentRecord = 0;
        OnAddHealth?.Invoke(currentHealth);
    }


    public void Dispose()
    {

    }


    public void RemoveHealth()
    {
        currentHealth -= 1;

        if (currentHealth > 0)
        {
            Debug.Log("Минус жизка");
            OnRemoveHealth?.Invoke();
            //soundProvider.PlayOneShot("FallEgg");
            return;
        }

        if (currentHealth == 0)
        {
            Debug.Log("Проигрыш");
            OnRemoveHealth?.Invoke();
            OnGameFailed?.Invoke();
            //soundProvider.PlayOneShot("Success");
            //particleEffectProvider.Play("Win");
        }
    }
    
    public void AddScore()
    {
        currentRecord += 1;
        OnChangeAllCountCoins?.Invoke(currentRecord);
        AddCoins(1);

        if(currentRecord == 30)
        {
            OnGameWinned?.Invoke();
        }
    }

    private void AddCoins(int coins)
    {
        moneyProvider.SendMoney(coins);
        OnGetCoins?.Invoke(coins);

    }
}
