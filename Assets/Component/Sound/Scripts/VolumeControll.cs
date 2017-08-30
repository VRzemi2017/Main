using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControll : MonoBehaviour {

	public AudioSource _soundCube1;

	// Use this for initialization
	void Start () {
		_soundCube1 = GameObject.Find ("SoundCube1").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			Debug.Log ( "Volumeを上げました" );
			_soundCube1.volume += 0.1f;
		}
		if (Input.GetKeyDown (KeyCode.J)) {
			Debug.Log ( "Volumeを下げました" );
			_soundCube1.volume -= 0.1f;
		}
	}
}
