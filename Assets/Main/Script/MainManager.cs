using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;
using System.Linq;

using MonobitNetwork = MonobitEngine.MonobitNetwork;

public class MainManager : MonoBehaviour {
    [SerializeField] MonobitServer server;
    [SerializeField] Messager message;
    [SerializeField] string titleName;

    public enum GameState
    {
        GAME_INIT,
        GAME_FADIN,

        GAME_START,
        GAME_PLAYING,
        GAME_RESULT,

        GAME_FADOUT,
        GAME_FINISH,
    }

    private static GameState _state = GameState.GAME_START;
    public static GameState CurrentState { get { return _state; } }

    public int PlayerNo 
    {
        get 
        {
            if (server && MonobitServer.PlayerNo >= 0)
            {
                return MonobitServer.PlayerNo;
            }

            return 0;
        }
    }

    private Subject<Unit> sceneLoaded = new Subject<Unit>();
    public IObservable<Unit> OnSceneLoaded { get { return sceneLoaded; } }

    private void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
        if (server)
        {
            server.OnEnterRoom.Subscribe(_ =>
            {
                LoadSceneAsync(titleName);
            }).AddTo(this);
        }
    }

    public void SetMessage(string msg)
    {
        if (message)
        {
            message.SetMessage(msg);
        }
    }

    public void LoadSceneAsync(string name)
    {
        SteamVR_LoadLevel.Begin(name);
    }
}
