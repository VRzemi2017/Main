using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoDisplay : MonoBehaviour {
    [SerializeField] TextMesh mesh;

    MainManager main;

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

        main = GameObject.FindObjectOfType<MainManager>();
        if (main)
        {
            main.AddPlayer(gameObject);
        }
	}

    private void OnDestroy()
    {
        if (main)
        {
            main.RemovePlayer(gameObject);
        }
    }
}
