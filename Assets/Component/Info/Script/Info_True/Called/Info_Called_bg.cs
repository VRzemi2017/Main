using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info_Called_bg : MonoBehaviour {
    float size;     //ウィンドウのサイズ
    float stretch_num = 0.1f;   //おまじない
    float stretch_speed = 1; //伸縮速度

    int text_timer;             //ウィンドウが伸び始めてから計測
                                //=========================================================================================//
    int display_time = 120;        //文字を表示し続ける時間(時間になったら文字が消える)
    int shrink_timing = 30;      //文字が消えてからウィンドウが縮み始めるまでの間
    int shrink_time = 0;                //文字が消えてからウィンドウが縮み始めるまでの間
                                        //=========================================================================================//
    int order_num;                  //0…伸びる、1…縮む

    public GameObject mes;             //表示するテキスト
    public GameObject parent;            //ウィンドウの親となる空のオブジェクト

    public TextMesh mes_text;

    // Use this for initialization
    void Start()
    {
        size = 0;
        text_timer = 0;
        order_num = 0;
        shrink_time = display_time + shrink_timing;

        transform.localScale = new Vector3(0, 0, 0);
        mes.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
        Extend();
    }

    void Extend()
    {        //ウィンドウが伸びる
        if (order_num == 0)
        {
            if (size < 1.0)
            {
                size += (stretch_num * stretch_speed);    //拡大
            }
            transform.localScale = new Vector3(size, size, size);     //反映

            if (size >= 1.0)
            {
                text_timer++;
                mes.SetActive(true);
            }
            else
            {
                mes.SetActive(false);
            }
        }

        if (text_timer >= display_time + 30)
        {
            mes.SetActive(false);
        }

        if (text_timer >= shrink_time)
        {
            order_num = 1;
            if (order_num == 1)
            {
                Shrink();
            }
        }

    }

    void Shrink()
    {        //ウィンドウが縮む
        if (size > 0)
        {
            size -= (stretch_num * stretch_speed);    //縮小
            transform.localScale = new Vector3(size, size, size);     //反映

            if (size <= 0.01)
            {   //サイズの初期化
                transform.localScale = new Vector3(0, 0, 0);
                Object.Destroy(parent);       //プレハブ消去
            }
        }
    }

    void UpdateText()
    {
        //mes_text.text = "タイムオーバー ";
    }
}
