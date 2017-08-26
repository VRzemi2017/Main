using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoStateStick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //くっつく型
        float info_pos_y = 0.01f;     //ウィンドウの高さ
        GameObject camera_stick = GameObject.Find("Camera (eye)");  //プレイヤーのカメラの取得
        Vector3 camera_pos_stick = camera_stick.transform.position;         //プレイヤーのカメラ座標
        Vector3 front_stick = camera_stick.transform.forward * 0.7f;        //カメラの正面*距離
        Quaternion camera_rot_stick = camera_stick.transform.rotation;      //プレイヤーのカメラの角度

        // position
        Vector3 pos_stick = camera_pos_stick + front_stick + new Vector3(0, info_pos_y, 0); //どの位置に出すか
                                                                                            // 回転
        Vector3 rot_stick = camera_rot_stick.eulerAngles;                               //どの角度で出すか
        rot_stick = new Vector3(0, rot_stick.y, 0);
        Quaternion qua_stick = Quaternion.Euler(rot_stick);

        this.transform.position = pos_stick;
        this.transform.rotation = qua_stick;
    }
}
