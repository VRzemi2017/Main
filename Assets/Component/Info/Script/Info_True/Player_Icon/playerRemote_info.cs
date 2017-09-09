using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRemote_info : MonoBehaviour {

    // Use this for initialization
    GameObject targetCamera;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        targetCamera = null;  //プレイヤーのカメラの取得
        this.transform.LookAt(this.targetCamera.transform.position);
        this.transform.Rotate(new Vector3(0f, 180f, 0f));
        
    }
}
