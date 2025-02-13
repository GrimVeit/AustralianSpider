using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggsPrefabs : MonoBehaviour
{
    [SerializeField] private List<EggPrefab> prefabs;

    private float totalWeight = 0;

    public void Initialize()
    {
       
    }

    public void Dispose()
    {

    }

    public EggPrefab GetRandomEgg()
    {
        int index = Random.Range(0, prefabs.Count);

        return prefabs[index];
    }
}
