using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UniRx;

public class SceneControler : MonoBehaviour
{
    AsyncOperation async;

    private Subject<Unit> sceneLoaded = new Subject<Unit>();
    public IObservable<Unit> OnSceneLoaded { get { return sceneLoaded; } }
    
    public string LoadingScene { get; private set; }
    public bool Loading { get; private set; }

    public void LoadSceneAsync(string name)
    {
        if (LoadingScene != null)
        {
            return;
        }

        StartCoroutine(LoadingAsync(name));
    }

    private IEnumerator LoadingAsync(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        LoadingScene = sceneName;
        Loading = true;
        async.allowSceneActivation = false;

        yield return new WaitWhile(() => async.progress < 0.9f);

        Loading = false;
        sceneLoaded.OnNext(Unit.Default);
    }

    public void DoChangeScene()
    {
        if (Loading == false && LoadingScene != null)
        {
            async.allowSceneActivation = true;
            LoadingScene = null;
        }
    }
}
