using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoDisplay : MonoBehaviour {
    [SerializeField] TextMesh mesh;

    // Use this for initialization
    void Start ()
    {
        MonobitEngine.MonobitView view = GetComponent<MonobitEngine.MonobitView>();
        if (view)
        {
            if (view.isMine)
            {
                mesh.gameObject.SetActive(false);
            }
            else
            {
                mesh.text = view.owner.ID + "P";
            }
        }
	}
}
