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
        int playerNo = 0;

        MainManager main = GameObject.FindObjectOfType<MainManager>();
        if (main)
        {
            playerNo = main.PlayerNo;
        }

        if (!player)
        {
            player = GameObject.FindObjectOfType<SteamVR_ControllerManager>().gameObject;
        }

        if (player && playerNo < startPosition.Count)
        {
            Vector3 shift = new Vector3();
            SteamVR_Camera eye = GameObject.FindObjectOfType<SteamVR_Camera>();
            if (eye)
            {
                shift = eye.gameObject.transform.localPosition;
            }

            player.transform.position = startPosition[playerNo].transform.position + shift;
            player.transform.rotation = startPosition[playerNo].transform.rotation;
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
