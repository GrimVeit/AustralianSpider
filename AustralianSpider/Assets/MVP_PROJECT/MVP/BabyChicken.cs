using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabyChicken : MonoBehaviour
{
    public event Action<BabyChicken> OnEndMove;

    [SerializeField] private Image chickenImage;
    [SerializeField] private Sprite spriteCrackEgg;
    [SerializeField] private Sprite spriteBreakEgg;
    [SerializeField] private Sprite[] spritesRun = new Sprite[3];

    private Transform moveOnFinishTransform;

    private IEnumerator handleEggSequence_IEnumerator;
    private IEnumerator playRunning_IEnumerator;

    private ISoundProvider soundProvider;

    private Tween moveTween;

    public void SetData(ISoundProvider soundProvider, Transform transform)
    {
        this.soundProvider = soundProvider;
        this.moveOnFinishTransform = transform;
    }

    public void ActivateAnimation()
    {
        chickenImage.enabled = true;

        if (handleEggSequence_IEnumerator != null)
            StopCoroutine(handleEggSequence_IEnumerator);

        if(playRunning_IEnumerator != null)
            StopCoroutine(playRunning_IEnumerator);


        handleEggSequence_IEnumerator = HandleEggSequence(0.1f, 0.3f);
        playRunning_IEnumerator = PlayRunningIEnumerator(0.02f);

        StartCoroutine(handleEggSequence_IEnumerator);
    }

    private IEnumerator HandleEggSequence(float timeCrack, float timeBreak)
    {
        chickenImage.sprite = spriteCrackEgg;

        soundProvider.PlayOneShot("Egg_Down");

        yield return new WaitForSeconds(timeCrack);

        chickenImage.sprite = spriteBreakEgg;

        yield return new WaitForSeconds(timeBreak);

        soundProvider.PlayOneShot("Egg_Run");

        MoveBabyChicken();
        StartCoroutine(playRunning_IEnumerator);
    }

    private IEnumerator PlayRunningIEnumerator(float timeDifferent)
    {
        int index = 0;

        while (true)
        {
            chickenImage.sprite = spritesRun[index];

            index = (index + 1) % spritesRun.Length;

            yield return new WaitForSeconds(timeDifferent);
        }
    }

    private void MoveBabyChicken()
    {
        if (moveTween != null) moveTween.Kill();

        moveTween = transform.DOMove(moveOnFinishTransform.position, 1.6f).OnComplete(() => OnEndMove?.Invoke(this));
    }

    private void OnDestroy()
    {
        if(handleEggSequence_IEnumerator != null)
            StopCoroutine(handleEggSequence_IEnumerator);

        if(playRunning_IEnumerator != null)
            StopCoroutine(playRunning_IEnumerator);

        moveTween?.Kill();
    }
}
