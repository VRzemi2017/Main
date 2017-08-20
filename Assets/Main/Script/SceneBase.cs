using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SceneBase : MonoBehaviour {

    [SerializeField]
    protected List<GameObject> startPosition;
    [SerializeField]
    protected GameObject player;
    [SerializeField]
    protected string sceneName;

    public enum GameState
    {
        GAME_INIT,
        GAME_FADIN,

        TITLE_START,

        MAIN_START,
        MAIN_PLAYING,
        MAIN_TIMEUP,
        MAIN_RESULT,

        GAME_FADOUT,
        GAME_FINISH,
    }

    protected GameState _state = GameState.GAME_INIT;
    public GameState CurrentState { get { return _state; } }

    protected Subject<GameState> stateChanged = new Subject<GameState>();
    public IObservable<GameState> OnStateChanged { get { return stateChanged; } }

    protected MainManager main;
    protected Messager message;

    public void ChangeState(GameState state)
    {
        SetMessage(state.ToString() + " state ending...");
        _state = state;
        SetMessage("Change to state " + state.ToString());
        stateChanged.OnNext(_state);
    }

    protected void InitPlayerPosition()
    {
        if (!main)
        {
            main = GameObject.FindObjectOfType<MainManager>();
        }
        
        if (player && main && main.PlayerNo < startPosition.Count)
        {
            player.transform.position = startPosition[main.PlayerNo].transform.position;
            player.transform.rotation = startPosition[main.PlayerNo].transform.rotation;
        }
    }

    protected void FadeIn()
    {
        FadeControl fad = GameObject.FindObjectOfType<FadeControl>();
        if (fad)
        {
            ChangeState(GameState.GAME_FADIN);

            fad.FadeIn();

            var fadDis = new SingleAssignmentDisposable();
            fadDis.Disposable = fad.OnFadeInEnd.Subscribe(i =>
            {
                FadeInEnd();
                fadDis.Dispose();
            }).AddTo(this);
        }
        else
        {
            FadeInEnd();
        }
    }

    protected virtual void FadeInEnd() { }

    protected void FadeOut()
    {
        var fadDis = new SingleAssignmentDisposable();
        var loadDis = new SingleAssignmentDisposable();

        FadeControl fad = GameObject.FindObjectOfType<FadeControl>();
        if (fad)
        {
            fad.FadeOut();
            fadDis.Disposable = fad.OnFadeOutEnd.Subscribe(i =>
            {
                DoChangeScene();
                fadDis.Dispose();
            }).AddTo(this);
        }

        if (!main)
        {
            main = GameObject.FindObjectOfType<MainManager>();
        }

        //if (main)
        //{
        //    scene.LoadScene(sceneName);
        //    loadDis.Disposable = scene.OnSceneLoaded.Subscribe(i =>
        //    {
        //        DoChangeScene();
        //        loadDis.Dispose();
        //    }).AddTo(this);
        //}
    }

    protected void DoChangeScene()
    {
        //FadeControl fad = GameObject.FindObjectOfType<FadeControl>();
        //if (fad && !fad.Fading && scene && !scene.Loading)
        //{
        //    scene.DoChangeScene();
        //}
    }

    protected void SetMessage(string msg)
    {
        if (message)
        {
            message.SetMessage(msg);
        }
    }
}
