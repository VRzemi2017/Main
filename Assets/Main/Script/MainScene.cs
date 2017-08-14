﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MainScene : MonoBehaviour {
    [SerializeField]
    private List<GameObject> startPosition;
    [SerializeField]
    private GameObject player;

	// Use this for initialization
	void Start () 
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Title")
        {
            MainManager.ChangeState(MainManager.GameState.GAME_TITLE);
        }

		InitPlayerPosition();
	}

    void InitPlayerPosition()
    {
        int PlayerNo = MainManager.PlayerNo;
        Debug.Log("PlayerNo: " + PlayerNo);

        if (player && PlayerNo >= 0 && PlayerNo < startPosition.Count)
        {
            player.transform.position = startPosition[PlayerNo].transform.position;
            player.transform.rotation = startPosition[PlayerNo].transform.rotation;
        }
    }
}
