using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

public class Info_switch : MonoBehaviour {
    //public GameObject text;             //表示するテキスト
    public TextMesh p1_state;     //表示するテキスト
    public TextMesh p2_state;     //表示するテキスト
    public TextMesh pairs_state;
    int state_p1 = 0;                      //0：準備中　1：準備完了
    int state_p2 = 0;
    int state_pairs = 0;

    // Use this for initialization
    void Start() {
        AdjustTitle();

        MainManager.OnStateChanged.Subscribe(s =>
        {
            switch (s)
            {
                case MainManager.GameState.GAME_NETWORK:
                    {
                        if (MainManager.PlayerNo == 0)
                        {
                            Player1Ready();
                        }
                        else
                        {
                            Player2Ready();
                        }
                    }
                    break;
                case MainManager.GameState.GAME_START:
                    {
                        Player1Ready();
                        Player2Ready();
                    }
                    break;
            }
        });
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F5))
        {
        }

        /////////////////////////////////////////////////////
        if ( Input.GetKeyDown( KeyCode.F1 ) ) {
            state_p1 = 1;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            state_p2 = 1;
        }
        if (state_p1 == 1 & state_p2 == 1)
        {
            state_pairs = 1;
        }
        UpdateText( );

        if ( state_pairs == 1 & Input.GetKeyDown( KeyCode.Return ) ) {
            SceneManager.LoadScene( 2 );
        }
	}

    public void Player1Ready()
    {
        state_p1 = 1;
    }
    public void Player2Ready()
    {
        state_p2 = 1;
    }

    void UpdateText( ) {    //テキストの中身
        if ( state_p1 == 0 ) {
            p1_state.text = "待機中";
        } else if ( state_p1 == 1 ) {
            p1_state.text = "準備完了";
        }

        if (state_p2 == 0)
        {
            p2_state.text = "待機中";
        } else if (state_p2 == 1)
        {
            p2_state.text = "準備完了";
        }

        if (state_pairs == 0)
        {
            pairs_state.text = "準備が完了するまでおまちください";
        }
        else if (state_pairs == 1)
        {
            pairs_state.text = "ゲームが始まります";
        }
    }

    private void AdjustTitle( )
    {
        float info_pos_y = 0.0f;     //ウィンドウの高さ
        GameObject camera = GameObject.Find("Camera (eye)");  //プレイヤーのカメラの取得
        Vector3 camera_pos = camera.transform.position;         //プレイヤーのカメラ座標
        Vector3 front = camera.transform.forward * 5f;        //カメラの正面*距離
        Quaternion camera_rot = camera.transform.rotation;      //プレイヤーのカメラの角度

        // position
        Vector3 pos = camera_pos + front + new Vector3(0, info_pos_y, 0); //どの位置に出すか
                                                                          // 回転
        Vector3 rot = camera_rot.eulerAngles;                               //どの角度で出すか
        rot = new Vector3(0, rot.y, 0);
        Quaternion qua = Quaternion.Euler(rot);

        this.transform.position = pos;        //ウィンドウのポジション
        this.transform.rotation = qua;        //ウィンドウの角度
    }
}
