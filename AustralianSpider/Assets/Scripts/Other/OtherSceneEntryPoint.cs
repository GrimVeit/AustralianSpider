using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private UIOtherSceneRoot sceneRootPrefab;

    private UIOtherSceneRoot sceneRoot;

    private ViewContainer viewContainer;
    //private WebViewPresenter otherWebViewPresenter;

    public void Run(UIRootView uIRootView)
    {
        Debug.Log("OPEN OTHER SCENE");

        sceneRoot = Instantiate(sceneRootPrefab);
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        //otherWebViewPresenter = new WebViewPresenter (new WebViewModel(), viewContainer.GetView<WebViewView>());
        //otherWebViewPresenter.Initialize();

        ActivateActions();
        //otherWebViewPresenter.GetLinkInTitleFromURL("https://rollingpoints.online/daily");
    }

    private void ActivateActions()
    {
        //otherWebViewPresenter.OnGetLinkFromTitle += GetUrl;
        //otherWebViewPresenter.OnFail += GoToMainMenu;
    }

    private void DeactivateActions()
    {
        //otherWebViewPresenter.OnGetLinkFromTitle -= GetUrl;
        //otherWebViewPresenter.OnFail -= GoToMainMenu;
    }

    private void GetUrl(string URL)
    {
        if(URL == null)
        {
            GoToMainMenu();
            return;
        }

        //otherWebViewPresenter.SetURL(URL);
        //otherWebViewPresenter.Load();
    }

    private void GoToMainMenu()
    {
        Debug.Log("NO GOOD, OPEN MAIN MENU");
        OnGoToMainMenu?.Invoke();
    }

    private void OnDestroy()
    {
        DeactivateActions();

        //otherWebViewPresenter.Dispose();
    }

    #region Input

    public event Action OnGoToMainMenu;

    #endregion
}
