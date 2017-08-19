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

    // Use this for initialization
    void Start () 
    {
        InitPlayerPosition();
        FadeIn();
    }
}
