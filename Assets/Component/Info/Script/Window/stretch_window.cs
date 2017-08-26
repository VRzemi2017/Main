using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stretch_window : MonoBehaviour {
    float size = 0;     //ウィンドウのサイズ
    float stretch_num = 0.1f;   //おまじない
    public float stretch_speed = 0; //伸縮速度

    int text_timer = 0;             //ウィンドウが伸び始めてから計測
    public int display_timing = 0;    //ウィンドウが開いてから文字が表示されるまでの間
    public int display_time = 0;        //文字を表示し続ける時間(時間になったら文字が消える)
    public int shrink_timing = 0;      //文字が消えてからウィンドウが縮み始めるまでの間
    int shrink_time = 0;                //文字が消えてからウィンドウが縮み始めるまでの間
    int order_num = 0;                  //0…伸びる、1…縮む

    public GameObject text;             //表示するテキスト
    public TextMesh stringTextMesh;     //表示するテキスト
    public GameObject parent;            //ウィンドウの親となる空のオブジェクト

    // Use this for initialization
    void Start () {
        shrink_time = display_timing + display_time + shrink_timing;

        transform.localScale = new Vector3( 0, 0, 0 );
        text.SetActive( false );
    }
	
	// Update is called once per frame
	void Update () {
        UpdateText( );
        Extend( );
	}

    void Extend( ) {        //ウィンドウが伸びる
        if ( order_num == 0 ) {
            if ( size < 1.0 ) {
                size += ( stretch_num * stretch_speed );    //拡大
            }
            transform.localScale = new Vector3( size, size, size );     //反映

            if ( size >= 1.0 ) {
                text_timer++;

                if ( display_timing <= text_timer && text_timer <= display_time ) {
                    text.SetActive( true );
                } else {
                    text.SetActive( false );
                }
            }
        }

        if ( text_timer >= shrink_time ) {
            order_num = 1;
            if ( order_num == 1 ) {
                Shrink( );
            }
        }

    }

    void Shrink( ) {        //ウィンドウが縮む
        if ( size > 0 ) {
            size -= ( stretch_num * stretch_speed );    //縮小
            transform.localScale = new Vector3( size, size, size );     //反映

            if ( size <= 0.01 ) {   //サイズの初期化
                transform.localScale = new Vector3( 0, 0, 0 );
                Object.Destroy( parent );       //プレハブ消去
            }
        }
    }

    void UpdateText( ) {    //テキストの中身
        stringTextMesh.text = "テレポしました。";
    }

	public void reset( ) {
		// 単一型の時しか呼ばない
		text_timer = 0;

	}
}
