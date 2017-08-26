using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class call_info : MonoBehaviour {
    //====================================================//

    public Transform Object;        //このスクリプトを貼り付けたオブジェクトを参照
    Vector3 object_pos;             //参照したオブジェクトの座標を引っ張り出す
    public float pos = 0.0f;        //オブジェクトとウィンドウの距離
    GameObject infoMgr;             //info_Mgrを探し、呼び出す

    //====================================================//

    // Use this for initialization
    void Start( ) {
        //==============================================//

        infoMgr = GameObject.Find( "Info_Mgr" );        //Info_Mgrを探す

        //==============================================//
    }

    // Update is called once per frame
    void Update( ) {
        if ( Input.GetKeyDown( KeyCode.C ) ) {
            //====================//

            Call( );        //ウィンドウを呼び出したいときに使う

            //====================//
        }
    }

    //===========================================================//
    void Call( ) {
        object_pos = Object.position;

        info_Mgr call = infoMgr.GetComponent<info_Mgr>( );
		//call.Generation( object_pos, pos );
    }

    //===========================================================//
}
