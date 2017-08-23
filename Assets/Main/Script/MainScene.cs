using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using UniRx.Triggers;

public class MainScene : SceneBase
{
    private void Awake()
    {
        MainManager.ChangeState(MainManager.GameState.GAME_INIT);
    }

    void Start () 
    {
        InitPlayerPosition();
        MainManager.ChangeState(MainManager.GameState.GAME_NETWORK);

        this.UpdateAsObservable().Subscribe(_ =>
        {
            SteamVR_ControllerManager m = GameObject.FindObjectOfType<SteamVR_ControllerManager>();
            if (m)
            {
                var leftObj = m.left.GetComponent<SteamVR_TrackedObject>();
                var rightObj = m.right.GetComponent<SteamVR_TrackedObject>();

                var left = (leftObj.index != SteamVR_TrackedObject.EIndex.None) ? SteamVR_Controller.Input((int)leftObj.index) : null;
                var right = (rightObj.index != SteamVR_TrackedObject.EIndex.None) ? SteamVR_Controller.Input((int)rightObj.index) : null;

                if ((left != null && left.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) || (right != null && right.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)))
                {
                    if (MainManager.CurrentState == MainManager.GameState.GAME_INIT || 
                        MainManager.CurrentState == MainManager.GameState.GAME_NETWORK ||
                        MainManager.CurrentState == MainManager.GameState.GAME_FINISH)
                    {
                        return;
                    }

                    int next = ((int)MainManager.CurrentState + 1) % (int)MainManager.GameState.GAME_STATE_MAX;
                    if (next == (int)MainManager.GameState.GAME_FINISH)
                    {
                        MainManager.LoadSceneAsync(sceneName);
                    }

                    MainManager.ChangeState((MainManager.GameState)next);
                }

            }
        });
    }
}
