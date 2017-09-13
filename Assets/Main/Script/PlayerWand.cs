using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWand : MonoBehaviour {

    GemController gem;

    // Use this for initialization
    void Start ()
    {
        gem = GetComponentInChildren<GemController>();
        MainManager.AddWand(gameObject, gem);
    }

    private void OnDestroy()
    {
        MainManager.RemoveWand(gameObject, gem);
    }
}
