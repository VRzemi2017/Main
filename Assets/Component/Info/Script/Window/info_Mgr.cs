using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class info_Mgr : MonoBehaviour {
    public GameObject message_window;   //呼び出すメッセージウィンドウ
    public GameObject state_window;   //ゲームスタート、ゲームオーバー
    public int state_num = 0;

    // Use this for initialization
    void Start( ) {
    }

    // Update is called once per frame
    void Update( ) {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            state_num = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            state_num = 1;
        }
    }
    
	public void Generat( ) {     //向きと位置の調節要必要
        float info_pos_y = -0.7f;     //ウィンドウの高さ
        GameObject camera = GameObject.Find( "Camera (eye)" );  //プレイヤーのカメラの取得
        Vector3 camera_pos = camera.transform.position;         //プレイヤーのカメラ座標
        Vector3 front = camera.transform.forward * 2.0f;        //カメラの正面*距離
        Quaternion camera_rot = camera.transform.rotation;      //プレイヤーのカメラの角度

        // position
        Vector3 pos = camera_pos + front + new Vector3( 0, info_pos_y, 0 ); //どの位置に出すか
		// 回転
		Vector3 rot = camera_rot.eulerAngles;                               //どの角度で出すか
		rot = new Vector3 (0, rot.y, 0);
		Quaternion qua = Quaternion.Euler (rot);
		// 生成
		Instantiate( message_window, pos, qua );        //呼び出すウィンドウを、どの位置で、どの角度で。
    }

    void State_call()
    {     //向きと位置の調節要必要
        float info_pos_y = -0.7f;     //ウィンドウの高さ
        GameObject camera = GameObject.Find("Camera (eye)");  //プレイヤーのカメラの取得
        Vector3 camera_pos = camera.transform.position;         //プレイヤーのカメラ座標
        Vector3 front = camera.transform.forward * 2.0f;        //カメラの正面*距離
        Quaternion camera_rot = camera.transform.rotation;      //プレイヤーのカメラの角度

        // position
        Vector3 pos = camera_pos + front + new Vector3(0, info_pos_y, 0); //どの位置に出すか
                                                                          // 回転
        Vector3 rot = camera_rot.eulerAngles;                               //どの角度で出すか
        rot = new Vector3(0, rot.y, 0);
        Quaternion qua = Quaternion.Euler(rot);
        // 生成
        Instantiate(state_window, pos, qua);        //呼び出すウィンドウを、どの位置で、どの角度で。
    }
}
