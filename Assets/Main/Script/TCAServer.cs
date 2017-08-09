using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UniRx;
using UniRx.Triggers;

public class TCAServer : MonobitEngine.MonoBehaviour {
    [SerializeField]
    private byte maxPlayer = 2;
    [SerializeField]
    private int updateStreamRate = 60;
    [SerializeField]
    private int sendRate = 30;
    [SerializeField]
    private float reconnectTime = 3;
	[SerializeField]
	private bool GUIDisplay = true;
	[SerializeField]
	private Color TestColor = Color.black;
    [SerializeField]
    private Messager message;

    const string SERVER_NAME = "TCA_SERVER";
    const string LOBBY_NAME = "TCA_LOBBY";
    const string ROOM_NAME = "TCA_MT_ROOM";

    private Subject<Unit> enterRoom = new Subject<Unit>();
    public IObservable<Unit> OnEnterRoom { get { return enterRoom; } }

    private void Start() {
        MonobitNetwork.updateStreamRate = updateStreamRate;
        MonobitNetwork.sendRate = sendRate;

        MonobitNetwork.autoJoinLobby = true;

        if (!MonobitNetwork.inRoom)
        {
            ConnectServer();
        }
    }

    private void OnDisconnectedFromServer()
    {
        message.SetMessage("Disconnected.");
        Debug.Log(message.Message.Value);
	    Observable.Timer(System.TimeSpan.FromSeconds(reconnectTime)).Subscribe(_ =>
        {
            ConnectServer ();
        });

    }

	private void ConnectServer()
	{
        message.SetMessage("Connecting Server...");
        Debug.Log(message.Message.Value);
	    MonobitNetwork.ConnectServer(SERVER_NAME);
	}

        private void OnJoinedLobby()
        {
            message.SetMessage("Enter Lobby.");
            Debug.Log(message.Message.Value);

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
            message.SetMessage("Enter Room.");
            Debug.Log(message.Message.Value);    

            enterRoom.OnNext(Unit.Default);
        }

	    private void OnGUI()
	    {
		    if (GUIDisplay) 
		    {
			    GUI.color = TestColor;
			    GUILayout.Label (MonobitNetwork.isConnect ? "Connected." : "Disconnected.");
			    GUILayout.Label (MonobitNetwork.inRoom ? "In Room." : "Not In Room.");
			    GUILayout.Label (MonobitNetwork.isHost ? "Host." : "Not Host.");	
		    }
	    }
    
}
