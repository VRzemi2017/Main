using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDataComponent : MonoBehaviour {

	public SoundManager.SoundData soundData;

	public GameObject a;

	void Start() {
		a = GameObject.Find ("a");
	}
	void Update() {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			ParentChanger.ExploreAudioSource ( a,this.gameObject  );
		}
	}
}
