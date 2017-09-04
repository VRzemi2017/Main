using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TextMesh mesh = GetComponent<TextMesh> ();

		MainManager main = GameObject.FindObjectOfType<MainManager> ();
		if (main) 
		{
			mesh.text = (main.PlayerNo + 1).ToString() + "P";
		}
	}
}
