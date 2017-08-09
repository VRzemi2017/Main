using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;
using System.Linq;

public class NetworkScene : MonoBehaviour {
    [SerializeField]
    private TCAServer server;
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private Messager message;

    public static int PlayerNo { get; private set; }

	void Start () {
        DontDestroyOnLoad(gameObject);
        server.OnEnterRoom.Subscribe(_ => 
        {
            message.SetMessage("Starting Game...");
            SceneManager.LoadScene(sceneName);
        }).AddTo(this);
	}
}
