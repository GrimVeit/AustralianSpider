using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : Chicken
{
    [SerializeField] private Transform eggToPosition;

    private NormalEgg currentEgg;

    private float moveTime;

    public override void SpawnEgg(EggPrefab prefab)
    {
        currentEgg = Instantiate(prefab.egg, spawnTransform) as NormalEgg;
        currentEgg.SetLocalPosition(spawnTransform.position);
        currentEgg.SetLocalRotation(Quaternion.identity);

        ActivateEvents(currentEgg);

        currentEgg.Initialize(prefab.eggValue);
        currentEgg.MoveTo(eggToPosition.position, moveTime);
        currentEgg.Rotate();
    }

    public override void SetMoveTime(float time)
    {
        moveTime = time;
    }
}
