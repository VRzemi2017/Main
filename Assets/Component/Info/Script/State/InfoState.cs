using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoState : MonoBehaviour {
    public TextMesh mes_text;
    
    info_mgr1 Info_mgr1;
    int state_number = 0;

    GameObject info_mgr;             //info_Mgrを探し、呼び出す

    // Use this for initialization
    void Start () {
        info_mgr = GameObject.Find("Info_Mgr");
        
    }

    // Update is called once per frame
    void Update()
    {
        Info_mgr1 = info_mgr.GetComponent<info_mgr1/*info_Mgr*/>();
        state_number = Info_mgr1.state_num;

        UpdateText();

        
    }

    void UpdateText( )
    {
        if (state_number == 0)
        {
            mes_text.text = "ゲームスタート ";
        }
        else if (state_number == 1)
        {
            mes_text.text = "タイムオーバー ";
        }
    }
}
