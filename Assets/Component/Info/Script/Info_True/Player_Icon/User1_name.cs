using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User1_name : MonoBehaviour {
    InfoManager infoManager;
    public TextMesh name_user1;
    GameObject infoMgr;
    // Use this for initialization
    void Start ()
    {
        infoMgr = GameObject.Find("InfoMgr");
    }
	
	// Update is called once per frame
	void Update () {
        infoManager = infoMgr.GetComponent<InfoManager>();
        name_user1.text = infoManager.player1_name.text;
    }
}
