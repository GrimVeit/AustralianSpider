using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FirstThrow_BotState : IBotState
{
    private IDiceRollProvider diceRollProvider;
    private IYatzyCombinationsProvider yatzyCombinationsProvider;

    private ChooseYatzyCombination yatzyCombination = new ChooseYatzyCombination();

    private int matchedCount = 0;
    private int unmatchedCount = 0;
    private List<int> matchedIndexes = new List<int>();

    public FirstThrow_BotState(IDiceRollProvider diceRollProvider, IYatzyCombinationsProvider yatzyCombinationsProvider)
    {
        this.diceRollProvider = diceRollProvider;
        this.yatzyCombinationsProvider = yatzyCombinationsProvider;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void EnterState()
    {
        Dictionary<int, YatzyCombinationData> yatzyCombinations = yatzyCombinationsProvider.YatzyCombinations();

        for (int i = 0; i < yatzyCombinations.Count; i++)
        {
            Debug.Log("Комбинация - " + yatzyCombinations[i] + ", количество вариантов - " + yatzyCombinations[i].NumbersCombinations.Count);
        }

        Debug.Log("АКТИВАЦИЯ СОСТОЯНИЯ ПЕРВОГО БРОСКА");

        HashSet<int> set2 = new HashSet<int>(diceRollProvider.Dices().Values.Select(d => d.Number).ToArray());

        float maxKPD = 0;
        int indexCombination = 0;
        int[] maxKPDArray = null;

        var combinations = yatzyCombinationsProvider.YatzyCombinations().
            Where(kv => !kv.Value.IsFreeze).
            ToDictionary(kv => kv.Key, kv => kv.Value);

        //Цикл всех комбинаций, например тройки, ятзи, фулл хаус и тд
        foreach (var kvp in combinations)
        {
            int[] currentMaxKPDArray = null;
            float maxKPDinCurrentCombination = 0;

            //Debug.Log("Выбор комбинации - " + kvp.Key);
            //Цикл всех массивов комбинаций, например 111, 12345 и тд

            foreach (int[] array in kvp.Value.NumbersCombinations)
            {
                //Debug.Log("Выбор чисел - ");

                int countMatch = ArrayComparer.CountMatches(array, diceRollProvider.Dices().Values.Select(d => d.Number).ToArray());
                float maxKPDInCurrentNumbers = 0;

                if (countMatch != 0)
                {
                    maxKPDInCurrentNumbers = countMatch * 1f / array.Length;
                }

                Debug.Log("Коэффициент - " + maxKPDInCurrentNumbers);

                if (maxKPDInCurrentNumbers > maxKPDinCurrentCombination)
                {
                    maxKPDinCurrentCombination = maxKPDInCurrentNumbers;
                    currentMaxKPDArray = array;
                }
            }

            if(maxKPDinCurrentCombination > maxKPD)
            {
                indexCombination = kvp.Key;
                maxKPD = maxKPDinCurrentCombination;
                maxKPDArray = currentMaxKPDArray;
            }
        }

        Debug.Log("Выбранная комбинация");

        if(maxKPDArray == null)
        {
            maxKPDArray = new int[] { 0, 0, 0, 0, 0 };
        }

        for (int i = 0; i < maxKPDArray.Length; i++)
        {
            Debug.Log(maxKPDArray[i]);
        }

        yatzyCombination.SetData
            (indexCombination, 
            maxKPDArray, 
            ArrayComparer.GetMatchDictionary(maxKPDArray, diceRollProvider.Dices().Values.Select(d => d.Number).ToArray()));

        Activate();
    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {

    }

    private void Activate()
    {
        Coroutines.Start(Test());
    }

    private IEnumerator Test()
    {

        yield return new WaitForSeconds(1);

        if (ContainsFalseInCombination(yatzyCombination.MatchDictionary))
        {
            foreach (var item in yatzyCombination.MatchDictionary)
            {
                if (item.Value)
                {
                    diceRollProvider.FreezeDice(item.Key);
                    yield return new WaitForSeconds(0.5f);
                }
            }

            diceRollProvider.StartRoll();
        }
        else
        {
            yatzyCombinationsProvider.FreezeCombination(yatzyCombination.IndexCombination);
            yield return new WaitForSeconds(2);
            yatzyCombinationsProvider.SubmitFreezeCombination();
            diceRollProvider.Reload();
        }
    }

    private bool ContainsFalseInCombination(Dictionary<int, bool> dictionary)
    {
        foreach(var value in dictionary.Values)
        {
            if (!value)
            {
                return true;
            }
        }

        return false;
    }
}
