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
        ChangeState(GameState.GAME_INIT);
    }

    void Start () 
    {
        InitPlayerPosition();
        FadeIn();

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

    protected override void FadeInEnd()
    {
        ChangeState(GameState.MAIN_START);
    }
}
