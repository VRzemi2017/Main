using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MainScene : MonoBehaviour {
    [SerializeField]
    private List<GameObject> startPosition;
    [SerializeField]
    private GameObject player;

	// Use this for initialization
	void Start () {
		InitPlayerPosition();
	}

    void InitPlayerPosition()
    {
        if (MonobitEngine.MonobitNetwork.inRoom)
        {
            int PlayerNo = MonobitEngine.MonobitNetwork.playerList.ToList().IndexOf(MonobitEngine.MonobitNetwork.player);
            Debug.Log("PlayerNo: " + PlayerNo);

            if (player && PlayerNo != -1 && PlayerNo < startPosition.Count)
            {
                player.transform.position = startPosition[PlayerNo].transform.position;
                player.transform.rotation = startPosition[PlayerNo].transform.rotation;
            }
        }
    }
}
