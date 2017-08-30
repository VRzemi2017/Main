using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class info_mgr1 : MonoBehaviour {

    //[SerializeField] GameObject callTest_win = null;        //他スクリプトから呼び出されたときに表示されるもの
    public int state_num = 0;

    int start_tmp = 0;
    int timeup_tmp = 0;
    int result_tmp= 0;
    int state_tmp = 0;

    int call_num = 0;

    public GameObject state_window;   //ゲームスタート、ゲームオーバー
    public GameObject state_stick;   //くっつくウィンドウ
    public GameObject result_window;        //リザルト画面

    public GameObject callTest_win = null;        //他スクリプトから呼び出されたときに表示されるもの

    void Start () {
        /*
		Instantiate (window_single);
		window_single = null;
		window_single = GameObject.Find ("Message_quad 2(Clone)");
        */

        start_tmp = (int)MainManager.GameState.GAME_START;
        timeup_tmp = (int)MainManager.GameState.GAME_TIMEUP;
        result_tmp = (int)MainManager.GameState.GAME_RESULT;
    }

    // Update is called once per frame
    void Update () {
        state_tmp = (int)MainManager.CurrentState;
        
        if ( call_num == 0 & state_tmp == start_tmp )
        {
            state_num = 0;
            State_stick();
            call_num = 1;
        } else if ( call_num == 1 & state_tmp == timeup_tmp)
        {
            state_num = 1;
            State_stick();
            call_num = 2;
        } else if ( call_num == 2 & state_tmp == result_tmp )
        {
            Result_call();
            call_num = 0;
        }

        //window.SetActive (false);

        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(state_stick);        //呼び出すウィンドウを、どの位置で、どの角度で。
        }

        if (Input.GetKeyDown(KeyCode.W)) {
			State_call();
		}
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Result_call();
        }
        */
        
    }

    public void call_info( ) {
		//window.SetActive (true);
        /*
		float window_y = -0.0f;		//ウィンドウの高さ
		GameObject camera = GameObject.Find ("Camera (eye)");
		Vector3 pos = camera.transform.position;
		Debug.Log ("camera" + pos);
		Vector3 front = camera.transform.forward * 2.5f;
		Quaternion camera_rot = camera.transform.rotation;

		// 位置
		window_single.transform.localPosition = pos + front + new Vector3( 0, window_y, 0 );

		// 回転
		Vector3 rot = camera_rot.eulerAngles;
		rot = new Vector3 (0, rot.y, 0);
		Quaternion qua = Quaternion.Euler (rot);
		window_single.transform.localRotation = qua;

		str_mgr1 call = GameObject.Find( "Message_bg" ).GetComponent<str_mgr1>( );
		call.reset ();
        */
	}

    void State_call()
    {     //向きと位置の調節要必要
        /*
        float info_pos_y = 0.05f;     //ウィンドウの高さ
        GameObject camera = GameObject.Find("Camera (eye)");  //プレイヤーのカメラの取得
        Vector3 camera_pos = camera.transform.position;         //プレイヤーのカメラ座標
        Vector3 front = camera.transform.forward * 0.5f;        //カメラの正面*距離
        Quaternion camera_rot = camera.transform.rotation;      //プレイヤーのカメラの角度

        // position
        Vector3 pos = camera_pos + front + new Vector3(0, info_pos_y, 0); //どの位置に出すか
                                                                          // 回転
        Vector3 rot = camera_rot.eulerAngles;                               //どの角度で出すか
        rot = new Vector3(0, rot.y, 0);
        Quaternion qua = Quaternion.Euler(rot);
        // 生成
        Instantiate(state_window, pos, qua);        //呼び出すウィンドウを、どの位置で、どの角度で。
        */
    }

    void State_stick()
    {
        // 生成
        Instantiate(state_stick);        //呼び出すウィンドウ
    }

    void Result_call( )
    {
        float info_pos_y = 0.06f;     //ウィンドウの高さ
        GameObject camera = GameObject.Find("Camera (eye)");  //プレイヤーのカメラの取得
        Vector3 camera_pos = camera.transform.position;         //プレイヤーのカメラ座標
        Vector3 front = camera.transform.forward * 0.65f;        //カメラの正面*距離
        Quaternion camera_rot = camera.transform.rotation;      //プレイヤーのカメラの角度

        // position
        Vector3 pos = camera_pos + front + new Vector3(0, info_pos_y, 0); //どの位置に出すか
                                                                          // 回転
        Vector3 rot = camera_rot.eulerAngles;                               //どの角度で出すか
        rot = new Vector3(0, rot.y, 0);
        Quaternion qua = Quaternion.Euler(rot);
        Instantiate(result_window, pos, qua);
    }

    public void Info_callWin( )
    {
        Instantiate(callTest_win);
    }
}
