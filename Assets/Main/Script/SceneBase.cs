using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SceneBase : MonoBehaviour {

    [SerializeField]
    protected List<GameObject> startPosition;
    [SerializeField]
    protected string sceneName;

    protected MainManager main;
    protected Messager message;

    GameObject player;

    protected void InitPlayerPosition()
    {
        if (!player)
        {
            player = GameObject.FindObjectOfType<SteamVR_ControllerManager>().gameObject;
        }

        if (player && MainManager.PlayerNo < startPosition.Count)
        {
            Vector3 shift = new Vector3();
            SteamVR_Camera eye = GameObject.FindObjectOfType<SteamVR_Camera>();
            if (eye)
            {
                shift = eye.gameObject.transform.localPosition;
            }

            player.transform.position = startPosition[MainManager.PlayerNo].transform.position + shift;
            player.transform.rotation = startPosition[MainManager.PlayerNo].transform.rotation;
        }
    }

    protected void SetMessage(string msg)
    {
        if (message)
        {
            message.SetMessage(msg);
        }
    }
}
