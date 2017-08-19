using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;
using System.Linq;

using MonobitNetwork = MonobitEngine.MonobitNetwork;

public class MainManager : MonoBehaviour {
    [SerializeField]
    private TCAServer server;
    [SerializeField]
    private SceneControler scene;
    [SerializeField]
    private string titleName;

    private void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
        server.OnEnterRoom.Subscribe(_ => 
        {
            SceneManager.LoadScene(titleName);
        }).AddTo(this);

        this.UpdateAsObservable().Subscribe(_ =>
        {
            SteamVR_ControllerManager m = GameObject.FindObjectOfType<SteamVR_ControllerManager>();
            if (m)
            {
                var leftObj = m.left.GetComponent<SteamVR_TrackedObject>();
                var rightObj = m.right.GetComponent<SteamVR_TrackedObject>();

                var left = (leftObj.index != SteamVR_TrackedObject.EIndex.None) ? SteamVR_Controller.Input((int)leftObj.index) : null;
                var right = (rightObj.index != SteamVR_TrackedObject.EIndex.None) ? SteamVR_Controller.Input((int)rightObj.index) : null;

                //SceneBase sb = GameObject.FindObjectOfType<SceneBase>();

                //if ((left != null && left.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) || (right != null && right.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)))
                //{
                //    if (_state == GameState.GAME_FADIN || _state == GameState.GAME_FINISH)
                //    {
                //        return;
                //    }

                //    int next = ((int)_state + 1) % (int)GameState.GAME_STATE_MAX;
                //    if (next == (int)GameState.GAME_FADIN)
                //    {
                //        ChangeScene(mainName);
                //    }

                //    if (next == (int)GameState.GAME_FINISH)
                //    {
                //        ChangeScene(titleName);
                //    }

                //    ChangeState((GameState)next);
                //}

            }
        });
    }

    private void ChangeScene(string sceneName)
    {
        var fadDis = new SingleAssignmentDisposable();
        var loadDis = new SingleAssignmentDisposable();

        FadControl fad = GameObject.FindObjectOfType<FadControl>();
        if (fad)
        {
            fad.Fadin();
            fadDis.Disposable = fad.OnFadinEnd.Subscribe(i =>
            {
                DoChangeScene();
                fadDis.Dispose();
            }).AddTo(this);
        }

        if (scene != null)
        {
            scene.LoadScene(sceneName);
            loadDis.Disposable = scene.OnSceneLoaded.Subscribe(i =>
            {
                DoChangeScene();
                loadDis.Dispose();
            }).AddTo(this);
        }
    }

    private void DoChangeScene()
    {
        FadControl fad = GameObject.FindObjectOfType<FadControl>();
        if (fad && !fad.Fading && scene && !scene.Loading)
        {
            scene.DoChangeScene();
        }
    }

    public static void SetMessage(string msg)
    {
        Messager message = GameObject.FindObjectOfType<Messager>();

        if (message)
        {
            message.SetMessage(msg);
        }
    }
}
