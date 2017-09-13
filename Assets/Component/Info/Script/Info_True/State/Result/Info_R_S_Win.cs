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
        //実機
        total_num = result.Spots.Length;

        //Debug
       /* List<string> spots = new List<string>();
        spots.Add("地底");
        spots.Add("天空");
        spots.Add("てんさい");
        //spots.Add("てんさい");
        //spots.Add("てんさい");
        total_num = spots.Count;*/

        win_size = 0.02f;
        win_y = -0.12f;
        this.transform.localScale += new Vector3(0, win_size * total_num, 0);
        this.transform.position += new Vector3(0, win_y * total_num, 0);
    }
}
