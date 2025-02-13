using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointAnimationView_BabyChicken : View, IPointAnimationView
{
    [SerializeField] private BabyChicken babyChickenPrefab;
    [SerializeField] private Transform parentSpawn;
    [SerializeField] private Transform transformEndMove;

    private List<BabyChicken> babyChickens = new List<BabyChicken>();

    private ISoundProvider soundProvider;

    public void PlayAnimation()
    {

    }

    public void PlayAnimation(Vector3 vector)
    {
        SpawnBabyChicken(vector);
    }

    private void SpawnBabyChicken(Vector3 vector)
    {
        BabyChicken babyChicken = Instantiate(babyChickenPrefab, parentSpawn);
        babyChicken.transform.SetPositionAndRotation(vector, babyChickenPrefab.transform.rotation);
        babyChicken.OnEndMove += DestroyBabyChicken;
        babyChicken.SetData(soundProvider, transformEndMove);
        babyChicken.ActivateAnimation();

        babyChickens.Add(babyChicken);
    }

    private void DestroyBabyChicken(BabyChicken babyChicken)
    {
        if(babyChicken != null)
        {
            babyChickens.Remove(babyChicken);
            babyChicken.OnEndMove -= DestroyBabyChicken;
            Destroy(babyChicken.gameObject);
        }
    }

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }
}
