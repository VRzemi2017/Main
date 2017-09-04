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
    [SerializeField] string titleName;

    public enum GameState
    {
        GAME_INIT,
        GAME_NETWORK,

        GAME_START,
        GAME_PLAYING,
        GAME_TIMEUP,
        GAME_RESULT,

        GAME_FINISH,

        GAME_STATE_MAX,
    }

    private static Messager message;

    private static GameState _state = GameState.GAME_START;
    public static GameState CurrentState { get { return _state; } }

    private static Subject<GameState> stateChanged = new Subject<GameState>();
    public static IObservable<GameState> OnStateChanged { get { return stateChanged; } }

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

    private static bool remoteReady;


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

            server.OnRemoteReady.Subscribe(_ =>
            {
                remoteReady = true;
                if (CurrentState == GameState.GAME_NETWORK)
                {
                    server.StartGame();
                }
            }).AddTo(this);

            server.OnStartGame.Subscribe(_ =>
            {
                ChangeState(GameState.GAME_START);
				NetworkObject[] networks = GameObject.FindObjectsOfType<NetworkObject>();
				networks.ToList().ForEach(g => g.enabled = true);
            }).AddTo(this);

            OnStateChanged.Subscribe(s =>
            {
                switch (s)
                {
                    case GameState.GAME_NETWORK:
                        {
                            ReadyToStart();
                        }
                        break;
                }
            }).AddTo(this);
        }
    }

    public static void ChangeState(GameState state)
    {
        SetMessage(_state.ToString() + " state ending...");
        _state = state;
        SetMessage("Change to state " + _state.ToString());
        stateChanged.OnNext(_state);
    }

    public static void SetMessage(string msg)
    {
        if (!message)
        {
            message = GameObject.FindObjectOfType<Messager>();
        }

        if (message)
        {
            message.SetMessage(msg);
        }
    }

    public static void LoadSceneAsync(string name)
    {
        remoteReady = false;
        SteamVR_LoadLevel.Begin(name);
    }

    public void ReadyToStart()
    {
        if (!remoteReady)
        {
            server.ReadyToStart();
        }
        else
        {
            server.StartGame();
        }
    }

    public static GameObject LocalPlayer()
    {
		return Camera.main.gameObject;
    }

    public static GameObject RemotePlayer()
    {
		return null;
    }
}
