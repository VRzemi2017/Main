using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;
using System.Linq;

using MonobitNetwork = MonobitEngine.MonobitNetwork;

public class MainManager : MonoBehaviour {
    [SerializeField]
    private TCAServer server;
    [SerializeField]
    private string titleName;
    [SerializeField]
    private string mainName;

    public enum GameState
    {
        GAME_INIT,
        GAME_NETWORK,
        GAME_TITLE,
        GAME_FADIN,
        GAME_START,
        GAME_PLAYING,
        GAME_TIMEUP,
        GAME_RESULT,
        GAME_FINISH,


        GAME_STATE_MAX,
    }
    private static GameState _state = GameState.GAME_INIT;
    public static GameState CurrentState { get { return _state; } }

    private void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        ChangeState(GameState.GAME_INIT);
    }

    void Start ()
    {
        ChangeState(GameState.GAME_NETWORK);
        
        server.OnEnterRoom.Subscribe(_ => 
        {
            if (CurrentState == GameState.GAME_NETWORK)
            {
                SceneManager.LoadScene(titleName);
            }
        }).AddTo(this);

        this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0)).Subscribe(_ => 
        {
            int next = ((int)_state + 1) % (int)GameState.GAME_STATE_MAX;
            if (next == (int)GameState.GAME_START)
            {
                SceneManager.LoadScene(mainName);
            }

            if (next == (int)GameState.GAME_INIT)
            {
                SceneManager.LoadScene(titleName);
                return;
            }

            ChangeState((GameState)next);
        });
	}

    public static void ChangeState(GameState state)
    {
        SetMessage(state.ToString() + " state ending...");
        _state = state;
        SetMessage( "Change to state " + state.ToString());
    }

    private static void SetMessage(string msg)
    {
        Messager message = GameObject.FindObjectOfType<Messager>();

        if (message)
        {
            message.SetMessage(msg); 
        }
    }
}
