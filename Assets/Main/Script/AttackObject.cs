using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


public class AttackObject : MonoBehaviour {
    public GameObject AttachObj;

	// Use this for initialization
	void Start () {
        this.LateUpdateAsObservable().Subscribe(_ => 
        {
            transform.position = AttachObj.transform.position;
            transform.rotation = AttachObj.transform.rotation;
            transform.localScale = AttachObj.transform.localScale;
        });
	}
}
