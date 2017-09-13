using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCreature : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    public void PlayAnimation(bool wait)
    {
      anim.SetTrigger(wait ? "Wait" : "Jump");
    }
}
