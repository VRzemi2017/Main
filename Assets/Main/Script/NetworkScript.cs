using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MonobitEngine.MonobitView))]
public class NetworkScript : MonoBehaviour {

    [SerializeField] bool all = true;
    [SerializeField] bool CheckByHost = true;
    [SerializeField] MonoBehaviour[] scripts;
    

    private void Awake()
    {
        if (CheckByHost && !MonobitEngineBase.MonobitNetwork.isHost)
        {
            DisableScript();
        }
        else
        {
            MonobitEngine.MonobitView view = GetComponent<MonobitEngine.MonobitView>();
            if (view && !view.isMine)
            {
                DisableScript();
            }
        }
    }

    private void DisableScript()
    {
        if (all)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                MonoBehaviour[] behaviours = transform.GetChild(i).GetComponentsInChildren<MonoBehaviour>();
                behaviours.ToList().ForEach(s => s.enabled = false);
            }
        }
        else
        {
            scripts.ToList().ForEach(s => s.enabled = false);
        }
    }
}
