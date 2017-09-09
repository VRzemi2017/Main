using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoDisplay : MonoBehaviour {
    [SerializeField] TextMesh mesh;

    // Use this for initialization
    void Start ()
    {
        MonobitEngine.MonobitView view = GetComponent<MonobitEngine.MonobitView>();
        if (view && MonobitEngineBase.MonobitNetwork.inRoom)
        {
            if (!view.isMine)
            {
                mesh.text = view.owner.ID + "P";
                mesh.gameObject.SetActive(true);
            }
        }
	}
}
