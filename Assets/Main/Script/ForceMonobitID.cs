using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMonobitID : MonoBehaviour {
    [SerializeField] int monobitID;

    private void Awake()
    {
        GetComponent<MonobitEngine.MonobitView>().viewID = monobitID;
    }
}
