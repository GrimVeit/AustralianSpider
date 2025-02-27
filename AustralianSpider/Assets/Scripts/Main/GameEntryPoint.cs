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

        yield return rootView.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MAIN_MENU);

        var sceneEntryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToGame += () => coroutines.StartCoroutine(LoadAndStartFromMenuToGame());
        sceneEntryPoint.OnGoToDailyTaskGame += () => coroutines.StartCoroutine(LoadAndStartFromMenuToDailyTaskGame());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartFromMenuToGame()
    {
        rootView.SetLoadScreen(2);

        yield return rootView.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.PORTRAIT_TO_LANDSCAPE);

        var sceneEntryPoint = Object.FindObjectOfType<PortraitToLandscapeSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToLandscapeScene += () => coroutines.StartCoroutine(LoadAndStartMiniGameScene());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartFromMenuToDailyTaskGame()
    {
        rootView.SetLoadScreen(2);

        yield return rootView.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.PORTRAIT_TO_LANDSCAPE);

        var sceneEntryPoint = Object.FindObjectOfType<PortraitToLandscapeSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToLandscapeScene += () => coroutines.StartCoroutine(LoadAndStartDailyTaskMiniGameScene());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartMiniGameScene()
    {
        rootView.SetLoadScreen(1);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MINI_GAME);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<MiniGameSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartFromGameToMenu());
        sceneEntryPoint.OnGoToGame += () => coroutines.StartCoroutine(LoadAndStartMiniGameScene());


        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartDailyTaskMiniGameScene()
    {
        rootView.SetLoadScreen(1);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.DAILY_TASK_MINI_GAME);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<DailyTaskGameSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartFromGameToMenu());


        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartFromGameToMenu()
    {
        rootView.SetLoadScreen(2);

        yield return rootView.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.LANDSCAPE_TO_PORTRAIT);

        var sceneEntryPoint = Object.FindObjectOfType<LandscapeToPortraitSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToPortraitSceneScene += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string scene)
    {
        Debug.Log("Загрузка сцены - " + scene);
        yield return SceneManager.LoadSceneAsync(scene);
    }
}
