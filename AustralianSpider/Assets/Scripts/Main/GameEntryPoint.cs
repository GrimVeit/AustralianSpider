using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    private static GameEntryPoint instance;
    private UIRootView rootView;
    private Coroutines coroutines;
    public GameEntryPoint()
    {
        coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIRootView>("UIRootView");
        rootView = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(rootView.gameObject);

    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Autorun()
    {
        GlobalGameSettings();

        instance = new GameEntryPoint();
        instance.Run();

    }

    private static void GlobalGameSettings()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Run()
    {
        coroutines.StartCoroutine(LoadAndStartMainMenu());
    }

    private IEnumerator LoadAndStartMainMenu()
    {
        rootView.SetLoadScreen(0);

        Screen.orientation = ScreenOrientation.Portrait;

        yield return rootView.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MAIN_MENU);

        var sceneEntryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToGame += () => coroutines.StartCoroutine(LoadAndStartMiniGameScene());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartMiniGameScene()
    {
        rootView.SetLoadScreen(1);

        Screen.orientation = ScreenOrientation.LandscapeLeft;

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MINI_GAME);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<MiniGameSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());
        sceneEntryPoint.OnGoToGame += () => coroutines.StartCoroutine(LoadAndStartMiniGameScene());


        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string scene)
    {
        Debug.Log("Загрузка сцены - " + scene);
        yield return SceneManager.LoadSceneAsync(scene);
    }
}
