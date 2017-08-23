using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SceneBase : MonoBehaviour {

    [SerializeField]
    protected List<GameObject> startPosition;
    [SerializeField]
    protected GameObject player;
    [SerializeField]
    protected string sceneName;

    protected MainManager main;
    protected Messager message;

    protected void InitPlayerPosition()
    {
        if (!main)
        {
            main = GameObject.FindObjectOfType<MainManager>();
        }
        
        if (player && main && main.PlayerNo < startPosition.Count)
        {
            player.transform.position = startPosition[main.PlayerNo].transform.position;
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
