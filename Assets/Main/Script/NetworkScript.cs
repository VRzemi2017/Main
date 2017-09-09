using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MonobitEngine.MonobitView))]
public class NetworkScript : MonoBehaviour {

    [SerializeField] MonoBehaviour[] scripts;

    private void Awake()
    {
        scripts.ToList().ForEach(s => s.enabled = false);
    }
}
