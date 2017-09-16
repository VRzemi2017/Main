using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSoundControll : MonoBehaviour {

	void Awake(){
		gameObject.GetComponent<AudioSource> ().Play();
		gameObject.GetComponent<AudioSource> ().Pause();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (MainManager.CurrentState == MainManager.GameState.GAME_RESULT) {
			gameObject.GetComponent<AudioSource> ().UnPause ();
		}
	}
}
