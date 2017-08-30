using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User2_name : MonoBehaviour {
    InfoManager infoManager;
    public TextMesh name_user2;
    GameObject infoMgr;
    // Use this for initialization
    void Start()
    {
        infoMgr = GameObject.Find("InfoMgr");
    }

    // Update is called once per frame
    void Update()
    {
        infoManager = infoMgr.GetComponent<InfoManager>();
        name_user2.text = infoManager.player2_name.text;
    }
}
