using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SceneBase : MonoBehaviour {

    [SerializeField]
    private List<GameObject> startPosition;

    public enum GameState
    {
        GAME_INIT,
        GAME_FADIN,
        GAME_START,
        GAME_PLAYING,
        GAME_TIMEUP,
        GAME_RESULT,
        GAME_FADOUT,
        GAME_FINISH,

        GAME_STATE_MAX,
    }

    protected GameState _state = GameState.GAME_INIT;
    public GameState CurrentState { get { return _state; } }

    protected Subject<GameState> stateChanged = new Subject<GameState>();
    public IObservable<GameState> OnStateChanged { get { return stateChanged; } }

    public void ChangeState(GameState state)
    {
        MainManager.SetMessage(state.ToString() + " state ending...");
        _state = state;
        MainManager.SetMessage("Change to state " + state.ToString());
        stateChanged.OnNext(_state);
    }

    protected void InitPlayerPosition()
    {
        SteamVR_ControllerManager player = GameObject.FindObjectOfType<SteamVR_ControllerManager>();
        if (player && TCAServer.PlayerNo >= 0 && TCAServer.PlayerNo < startPosition.Count)
        {
            player.transform.position = startPosition[TCAServer.PlayerNo].transform.position;
            player.transform.rotation = startPosition[TCAServer.PlayerNo].transform.rotation;
        }
    }

    protected void FadeIn()
    {
        FadControl fad = GameObject.FindObjectOfType<FadControl>();
        if (fad)
        {
            var fadDis = new SingleAssignmentDisposable();
            ChangeState(GameState.GAME_FADIN);
            fad.Fadin();

            fadDis.Disposable = fad.OnFadinEnd.Subscribe(i =>
            {
                ChangeState(GameState.GAME_START);
                fadDis.Dispose();
            }).AddTo(this);
        }
    }
}
