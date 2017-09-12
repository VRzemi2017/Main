using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC : MonoBehaviour {

    Gem cc;

    // Use this for initialization
    void Start () {
        cc = GetComponent<Gem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.T))
        {
            GemController gg = GameObject.FindObjectOfType<GemController>();
            if (gg)
            {
                cc.HitByPlayer(_ => {
                    gg.GetGemAction(_);
                });
            }
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            GemController gg = GameObject.FindObjectOfType<GemController>();
            if (gg)
            {
                cc.HitPlayerLeave();
            }
        }
    }
}
