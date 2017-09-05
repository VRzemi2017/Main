using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotControl : MonoBehaviour {

    private bool reached;
    public bool IsReached { get { return reached; } }

    private void OnTriggerEnter(Collider other)
    {
        reached = true;
        GetComponent<Collider>().enabled = false;
    }
}
