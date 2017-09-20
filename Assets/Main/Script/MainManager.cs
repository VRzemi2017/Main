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

    private static GameObject localPlayer = null;
    public static GameObject LocalPlayer { get { return localPlayer; } }
    private static List<GameObject> players = new List<GameObject>();
    private static GemController localWand = null;
    public static GemController LocalWand { get { return localWand; } }
    private static GemController remoteWand = null;
    public static GemController RemoteWand { get { return remoteWand; } }
    private static List<GemController> playerWands = new List<GemController>();

    private static Messager message;

    private static GameState _state = GameState.GAME_INIT;
    public static GameState CurrentState { get { return _state; } }

    private static Subject<GameState> stateChanged = new Subject<GameState>();
    public static IObservable<GameState> OnStateChanged { get { return stateChanged; } }

    private static Subject<EventData> eventHappaned = new Subject<EventData>();
    public static IObservable<EventData> OnEventHappaned { get { return eventHappaned; } }

    private static MainManager instance;
    public static MainManager Instance { get { return instance; } }

    public MonobitServer Server { get { return server; } }

    public static int PlayerNo 
    {
        get 
        {
            if (MonobitServer.PlayerNo >= 0)
            {
                return MonobitServer.PlayerNo;
            }

            return 0;
        }
    }

    private static bool remoteReady;

    private System.IDisposable dis;

    private void Awake() 
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
        if (server)
        {
            dis = server.OnEnterRoom.Subscribe(_ =>
            {
                LoadSceneAsync(titleName);
                dis.Dispose( );
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
                remoteReady = false;
                ChangeState(GameState.GAME_START);
            }).AddTo(this);

            server.OnRecieveEvent.Subscribe(e =>
            {
                switch (e.gameEvent)
                {
                    case GameEvent.EVENT_HIT_GEM:
                        {
                            e.eventObject.GetComponent<Gem>().HitByPlayer(g => remoteWand.GetGemAction(g));
                        }
                        break;
                    case GameEvent.EVENT_LEAVR_GEM:
                        {
                            e.eventObject.GetComponent<Gem>().HitPlayerLeave();
                        }
                        break;
                    case GameEvent.EVENT_GEM:
                        {
                            e.eventObject = e.eventObject.GetComponentInChildren<GemController>().gameObject;
                            eventHappaned.OnNext(e);
                        }
                        break;
                    case GameEvent.EVENT_DAMAGE:
                        {
                            e.eventObject = localPlayer;
                            eventHappaned.OnNext(e);
                        }
                        break;
                    case GameEvent.EVENT_ENEMY_JUMP:
                        {
                            e.eventObject.GetComponent<NetworkCreature>().PlayAnimation(false);
                        }
                        break;
                    case GameEvent.EVENT_ENEMY_WAIT:
                        {
                            e.eventObject.GetComponent<NetworkCreature>().PlayAnimation(true);
                        }
                        break;
                }
            }).AddTo(this);
        }

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

    public static void EventTriggered(EventData e)
    {
        if (   e.gameEvent == GameEvent.EVENT_HIT_GEM 
            || e.gameEvent == GameEvent.EVENT_LEAVR_GEM
            || e.gameEvent == GameEvent.EVENT_DAMAGE
            || e.gameEvent == GameEvent.EVENT_ENEMY_JUMP
            || e.gameEvent == GameEvent.EVENT_ENEMY_WAIT
            )
        {
            if (Instance)
            {
                Instance.Server.SendEvent(e);
            }
        }

        eventHappaned.OnNext(e);

        //after OnNext, because change eventObject
        if (e.gameEvent == GameEvent.EVENT_GEM)
        {
            if (Instance)
            {
                e.eventObject = e.eventObject.GetComponentInParent<PlayerWand>().gameObject;
                Instance.Server.SendEvent(e);
            }
        }
    }

    public static void LoadSceneAsync(string name)
    {
        ChangeState(GameState.GAME_FINISH);
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

	public static GameObject[] GetPlayers()
    {
        return players.ToArray();
    }

	public static void AddPlayer(GameObject obj)
    {
        if (obj && !players.Contains(obj))
        {
            players.Add(obj);

            if (!Instance || MonobitServer.IsLocalObj(obj))
            {
                localPlayer = obj;
            }
        }
    }

	public static void RemovePlayer(GameObject obj)
    {
        if (obj && players.Contains(obj))
        {
            players.Remove(obj);

            if (!Instance || MonobitServer.IsLocalObj(obj))
            {
                localPlayer = null;
            }
        }
    }

    public static void AddWand(GameObject obj, GemController c)
    {
        if (obj && c && !playerWands.Contains(c))
        {
            playerWands.Add(c);
            if (Instance && !MonobitServer.IsLocalObj(obj))
            {
                remoteWand = c;
            }
            else
            {
                localWand = c;
            }
        }
    }

    public static void RemoveWand(GameObject obj, GemController c)
    {
        if (obj && c && playerWands.Contains(c))
        {
            playerWands.Remove(c);

            if (Instance && !MonobitServer.IsLocalObj(obj))
            {
                remoteWand = null;
            }
            else
            {
                localWand = null;
            }
        }
    }
}
