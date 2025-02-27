using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitToLandscapeSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UILandscapePortraitSceneRoot sceneRootPrefab;

    private UILandscapePortraitSceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private BankPresenter bankPresenter;

    private PhoneVisualizePresenter phoneVisualizePresenter;

    private IEnumerator changeOrientationCoro;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = sceneRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        sceneRoot.Activate();

        soundPresenter = new SoundPresenter(new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS), viewContainer.GetView<SoundView>());
        soundPresenter.Initialize();

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Initialize();

        particleEffectPresenter = new ParticleEffectPresenter(new ParticleEffectModel(), viewContainer.GetView<ParticleEffectView>());
        particleEffectPresenter.Initialize();

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        bankPresenter.Initialize();

        phoneVisualizePresenter = new PhoneVisualizePresenter(new PhoneVisualizeModel(), viewContainer.GetView<PhoneVisualizeView>());
        phoneVisualizePresenter.Initialize();

        ActivateEvents();

        phoneVisualizePresenter.PortraitToLandscape();
    }

    private void ActivateEvents()
    {
        phoneVisualizePresenter.OnCompleteMoveFromPortraitToLandscape += ActivateChangeOrientation;
    }

    private void DeactivateEvents()
    {
        phoneVisualizePresenter.OnCompleteMoveFromPortraitToLandscape -= ActivateChangeOrientation;
    }

    private void ActivateChangeOrientation()
    {
        if(changeOrientationCoro != null)
            Coroutines.Stop(changeOrientationCoro);

        changeOrientationCoro = ChangeOrientation();
        Coroutines.Start(changeOrientationCoro);
    }

    private IEnumerator ChangeOrientation()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        yield return new WaitForSeconds(0.05f);

        HandleGoToLandscapeScene();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    phoneVisualizePresenter.LandscapeToPortrait();
        //}

        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    phoneVisualizePresenter.PortraitToLandscape();
        //}
    }

    public void Dispose()
    {
        DeactivateEvents();

        sceneRoot?.Dispose();
        soundPresenter?.Dispose();
        bankPresenter?.Dispose();

        phoneVisualizePresenter?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input

    public event Action OnGoToLandscapeScene;

    private void HandleGoToLandscapeScene()
    {
        sceneRoot.Deactivate();
        soundPresenter.Dispose();

        OnGoToLandscapeScene?.Invoke();
    }

    #endregion
}
