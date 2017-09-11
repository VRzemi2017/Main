using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int tmp = (int)MainManager.CurrentState;

            tmp = (tmp + 1) % (int)(MainManager.GameState.GAME_STATE_MAX);

            MainManager.ChangeState((MainManager.GameState)tmp);

            Debug.Log(MainManager.CurrentState);
        }
    }
}
