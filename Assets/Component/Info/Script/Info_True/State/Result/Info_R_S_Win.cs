using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info_R_S_Win : MonoBehaviour {
    private int total_num;
    private float win_size;
    private float win_y;
	// Use this for initialization
	void Start () {
        ResultManager result = GameObject.FindObjectOfType<ResultManager>();
        total_num = result.Spots.Length;

        win_size = 0.03f;
        win_y = -0.14f;
        this.transform.localScale += new Vector3(0, win_size * total_num, 0);
        this.transform.position += new Vector3(0, win_y * total_num, 0);
    }
	
	// Update is called once per frame
	void Update () {
	}
}
