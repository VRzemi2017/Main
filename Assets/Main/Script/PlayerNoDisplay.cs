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
                mesh.text = (MainManager.PlayerNo + 1)  + "P";
                mesh.gameObject.SetActive(true);
            }
        }

		MainManager.AddPlayer(gameObject);
	}

    private void OnDestroy()
    {
		MainManager.RemovePlayer(gameObject);
    }
}
