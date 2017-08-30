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

        this.UpdateAsObservable().Subscribe(_ =>
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (MainManager.CurrentState == MainManager.GameState.GAME_START ||
                    MainManager.CurrentState == MainManager.GameState.GAME_NETWORK ||
                    MainManager.CurrentState == MainManager.GameState.GAME_PLAYING)
                {
                    MainManager.LoadSceneAsync(sceneName);
                    MainManager.ChangeState(MainManager.GameState.GAME_FINISH);
                }
            }
        });
    }
}
