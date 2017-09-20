using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class InfoManager : MonoBehaviour {

    //infomation呼び出し用
    public enum InfoCase
    {
        INFO_WAIT,  //待機
        INFO_TEST,  //テスト用
        
        INFO_DAMAGE,
        INFO_GET,

        INFO_CASE_MAX,
    }
    public static InfoCase Info_Case = InfoCase.INFO_WAIT;      //待機
    public GameObject called_window;           //他スクリプトから呼び出されるウィンドウ
    public TextMesh info_text;

    //ゲームスタート、タイムアップ、リザルト呼び出し用
    public GameObject state_window;   //ゲームスタート、タイムアップ用のウィンドウ
    public GameObject result_window;  //リザルト画面用のウィンドウ
    int state_tmp = 0;          //現在のGameStateを参照
    int start_tmp;          //スタート
    int timeup_tmp;         //タイムアップ
    int result_tmp;         //リザルト
    public int state_pattern = 0;  //スタートとタイムアップの使い分け用
    int tmp_case = 0;       //３ケースの使い分け用
    public int pickup_score; //宝石を拾った時のスコア（器）
    public int rob_score;// 宝石を取られた時のスコア（器）
    public static int pickup; 　// 宝石を拾った時のスコア
    public static int rob; // 宝石を取られた時のスコア
    public static int teleport; //テレポテト回数
    // Use this for initialization
    void Start () {
        //MainManagerからGameStateを参照
        start_tmp = (int)MainManager.GameState.GAME_START;
        timeup_tmp = (int)MainManager.GameState.GAME_TIMEUP;
        result_tmp = (int)MainManager.GameState.GAME_RESULT;

        MainManager.OnEventHappaned.Subscribe(e =>
        {
            switch (e.gameEvent)
            {
                case GameEvent.EVENT_GEM:
                    {
                        if (MainManager.RemoteWand && e.eventObject == MainManager.RemoteWand.gameObject)
                        {
                            break;
                        }
                        CallCase(InfoCase.INFO_GET);
                    }
                    break;
                case GameEvent.EVENT_DAMAGE:
                    {
                        Debug.Log("Damaged player: " + e.eventObject.GetComponent<MonobitEngine.MonobitView>().viewID);

                        if (MainManager.LocalPlayer == e.eventObject)
                        {
                            Debug.Log("Show Damage: " + MainManager.LocalPlayer.GetComponent<MonobitEngine.MonobitView>().viewID);
                            CallCase(InfoCase.INFO_DAMAGE);
                        }
                    }
                    break;
            }
        }).AddTo(this);
    }
	
	// Update is called once per frame
	void Update () {
        //スタート、タイムアップ、リザルトを呼び出す
        state_tmp = (int)MainManager.CurrentState;

        if (tmp_case == 0 & state_tmp == start_tmp)
        {
            state_pattern = 0;
            State_Call();
            tmp_case = 1;
        }
        else if (tmp_case == 1 & state_tmp == timeup_tmp)
        {
            state_pattern = 1;
            State_Call();
            tmp_case = 2;
        }
        else if (tmp_case == 2 & state_tmp == result_tmp)
        {
            Result_Call();
            tmp_case = 0;
        }

        //infomationを呼び出す
        if (Info_Case == InfoCase.INFO_TEST)
        {

        }
        if (Info_Case == InfoCase.INFO_DAMAGE)
        {
            Info_Rob();
        }
        if (Info_Case == InfoCase.INFO_GET)
        {
            Info_Pickup();
        }
    }

    void State_Call()   //スタート、タイムアップのウィンドウを呼び出す
    {
        // 生成
        Instantiate(state_window);        //呼び出すウィンドウ
    }

    void Result_Call()
    {
        Instantiate(result_window);     //呼び出すウィンドウ

    }

    void Info_Called()
    {
        Instantiate(called_window);     //呼び出すウィンドウ
    }

    void Info_Rob()
    {
        Info_Called();
        Info_Case = InfoCase.INFO_WAIT;
        info_text.text = ("敵に噛みつかれた");
        rob += rob_score;
    }

    void Info_Pickup()
    {
        Info_Called();
        Info_Case = InfoCase.INFO_WAIT;
        info_text.text = ("魔法石を拾った");
        pickup += pickup_score;
    }

    public static void CallCase(InfoCase state)     //他スクリプトからのinfomation呼び出し用
    {
        Info_Case = state;
    }
}
