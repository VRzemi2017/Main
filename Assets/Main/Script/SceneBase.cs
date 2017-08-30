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
        if (!main)
        {
            main = GameObject.FindObjectOfType<MainManager>();
        }

        if (!player)
        {
            player = GameObject.FindObjectOfType<SteamVR_ControllerManager>().gameObject;
        }

        if (player && main && main.PlayerNo < startPosition.Count)
        {
            Vector3 shift = new Vector3();
            SteamVR_Camera eye = GameObject.FindObjectOfType<SteamVR_Camera>();
            if (eye)
            {
                shift = eye.gameObject.transform.localPosition;
            }

            player.transform.position = startPosition[main.PlayerNo].transform.position + shift;
            player.transform.rotation = startPosition[main.PlayerNo].transform.rotation;
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
