using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCall_test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            InfoManager.CallCase(InfoManager.InfoCase.INFO_DAMAGE);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            InfoManager.CallCase(InfoManager.InfoCase.INFO_GET);
        }
    }
}
