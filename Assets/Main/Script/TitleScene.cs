using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using UniRx.Triggers;

public class TitleScene : SceneBase
{
    private void Awake()
    {
        MainManager.ChangeState(MainManager.GameState.GAME_INIT);
    }

    void Start()
    {
        InitPlayerPosition();
        MainManager.ChangeState(MainManager.GameState.GAME_NETWORK);

        if (MonobitServer.OffLine)
        {
            Observable.Timer(System.TimeSpan.FromSeconds(1)).Subscribe(_ =>
            {
                MainManager.ChangeState(MainManager.GameState.GAME_START);
            });
        }

        this.UpdateAsObservable().Subscribe(_ =>
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ToMain();    
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                MainScene.ForceLevel(0);
                ToMain();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                MainScene.ForceLevel(1);
                ToMain();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                MainScene.ForceLevel(2);
                ToMain();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                MainScene.ForceLevel(3);
                ToMain();
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                MainScene.ForceLevel(4);
                ToMain();
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                MainScene.ForceLevel(5);
                ToMain();
            }
        });
    }

    private void ToMain()
    {
        if (MainManager.CurrentState == MainManager.GameState.GAME_START)
        {
            MainManager.LoadSceneAsync(sceneName);
        }
    }
}
