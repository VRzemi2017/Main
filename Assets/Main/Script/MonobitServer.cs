using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class MonobitServer : MonobitEngine.MonoBehaviour {
    [SerializeField]
    private byte maxPlayer = 2;
    [SerializeField]
    private int updateStreamRate = 60;
    [SerializeField]
    private int sendRate = 30;
    [SerializeField]
    private float retryTime = 3;
	[SerializeField]
	private bool GUIDisplay = true;
	[SerializeField]
	private Color TestColor = Color.black;
    [SerializeField]
    private Messager message;
    [SerializeField]
    private bool offline = true;
    //[SerializeField] private string roomName = "TCA_JACK_ROOM";

    const string SERVER_NAME = "TCA_SERVER";
    const string LOBBY_NAME = "TCA_LOBBY";

    private Subject<Unit> enterRoom = new Subject<Unit>();
    public IObservable<Unit> OnEnterRoom { get { return enterRoom; } }

    private Subject<Unit> remoteReady = new Subject<Unit>();
    public IObservable<Unit> OnRemoteReady { get { return remoteReady; } }

    private Subject<Unit> checkRemoteReady = new Subject<Unit>();
    public IObservable<Unit> OnCheckRemoteReady { get { return checkRemoteReady; } }

    private Subject<Unit> recieveStart = new Subject<Unit>();
    public IObservable<Unit> OnStartGame { get { return recieveStart; } }

    private Subject<EventData> recieveEvent = new Subject<EventData>();
    public IObservable<EventData> OnRecieveEvent { get { return recieveEvent; } }

    public static bool OffLine { get { return Instance == null || Instance.offline; } }

    private static MonobitServer instance;
    public static MonobitServer Instance { get { return instance; } }

    private static int playerNo = -1;
    public static int PlayerNo { get { return playerNo; } }

    private System.IDisposable dis;

    private void Start() {
        instance = this;

        if (offline)
        {
            MonobitView mv = GetComponent<MonobitView>();
            DestroyObject(mv);

            enterRoom.OnNext(Unit.Default);
        }
        else
        {
            MonobitNetwork.updateStreamRate = updateStreamRate;
            MonobitNetwork.sendRate = sendRate;

            MonobitNetwork.autoJoinLobby = true;

            if (!MonobitNetwork.inRoom)
            {
                ConnectServer();
            }
        }
    }

    private void OnDisconnectedFromServer()
    {
        SetMessage("Disconnected.");
	    Observable.Timer(System.TimeSpan.FromSeconds(retryTime)).Subscribe(_ =>
        {
            ConnectServer ();
        });
    }

	private void ConnectServer()
	{
        SetMessage("Connecting Server...");
	    MonobitNetwork.ConnectServer(SERVER_NAME);
	}

    private void OnJoinedLobby()
    {
        SetMessage("Enter Lobby.");

        MonobitEngine.RoomSettings settings = new MonobitEngine.RoomSettings();
        settings.maxPlayers = maxPlayer;
        settings.isVisible = true;
        settings.isOpen = true;
        MonobitEngine.LobbyInfo lobby = new MonobitEngine.LobbyInfo();
        lobby.Kind = LobbyKind.Default;
        lobby.Name = LOBBY_NAME;
        MonobitEngine.MonobitNetwork.JoinOrCreateRoom(LOBBY_NAME, settings, lobby);
    }

    private void OnJoinedRoom()
    {
        SetMessage("Enter Room.");

        UpdatePlayerNo();

        enterRoom.OnNext(Unit.Default);
    }

    private void UpdatePlayerNo()
    {
        playerNo = MonobitNetwork.playerCountInRoom - 1;
        SetMessage("Player: " + MonobitNetwork.playerCountInRoom + "P");
    }

	private void OnGUI()
	{
	    if (GUIDisplay) 
	    {
	        GUI.color = TestColor;
	        GUILayout.Label (MonobitNetwork.isConnect ? "Connected." : "Disconnected.");
	        GUILayout.Label (MonobitNetwork.inRoom ? "In Room." : "Not In Room.");
	        GUILayout.Label (MonobitNetwork.isHost ? "Host." : "Not Host.");	
            GUILayout.Label ("Player: " + (playerNo + 1) + "P");	
	    }
	}

    private void SetMessage(string msg)
    {
        if (message)
        {
            message.SetMessage(msg); 
        }
    }

    public void ReadyToStart()
    {
        if (offline)
        {
            RecieveStart();
            return;
        }

        if (monobitView)
        {
            if (!MonobitNetwork.isHost)
            {
                monobitView.RPC("RemoteReady", MonobitTargets.Host);
            }
            else
            {
                dis = Observable.Interval(System.TimeSpan.FromSeconds(retryTime)).Subscribe(_ =>
                {
                    monobitView.RPC("CheckRemoteReady", MonobitTargets.Others);
                });
            }
        }
    }

    public void StartGame()
    {
        if (monobitView)
        {
            if (MonobitNetwork.isHost)
            {
                monobitView.RPC("RecieveStart", MonobitTargets.All);
            }
        }
    }

    public static bool IsLocalObj(GameObject obj)
    {
        if (obj)
        {
            MonobitView view = obj.GetComponent<MonobitView>();
            if (view && view.owner != null)
            {
                return view.isMine;
            }
        }

        return false;
    }

    public void SendEvent(EventData data)
    {
        if (data.eventObject)
        {
            MonobitView view = data.eventObject.GetComponent<MonobitView>();
            if (view && monobitView)
            {
                if (data.gameEvent == GameEvent.EVENT_DAMAGE)
                {
                    Debug.Log("EVENT_DAMAGE SendEvent: " + data.gameEvent + " ID: " + view.viewID);
                }
                
                monobitView.RPC("RecieveEvent", MonobitTargets.Others, (int)data.gameEvent, view.viewID);
            }
        }
    }

    [MunRPC]
    void RemoteReady()
    {
        if (dis != null)
        {
            dis.Dispose();
            dis = null;
        }

        remoteReady.OnNext(Unit.Default);
    }

    [MunRPC]
    void RecieveStart()
    {
        recieveStart.OnNext(Unit.Default);
    }

    [MunRPC]
    void CheckRemoteReady()
    {
        checkRemoteReady.OnNext(Unit.Default);
    }

    [MunRPC]
    void RecieveEvent(int e, int ID)
    {
        

        EventData data = new EventData() { gameEvent = (GameEvent)e };

        MonobitView[] views = GameObject.FindObjectsOfType<MonobitView>();
        views.ToList().ForEach(v => 
        {
            if (v.viewID == ID)
            {
                data.eventObject = v.gameObject;
            }
        });

        if ((GameEvent)e == GameEvent.EVENT_DAMAGE)
        {
             Debug.Log("EVENT_DAMAGE RecieveEvent: " + (GameEvent)e + " ID: " + ID + " RealID: " + data.eventObject.GetComponent<MonobitView>().viewID);
        }

        recieveEvent.OnNext(data);
    }
}
