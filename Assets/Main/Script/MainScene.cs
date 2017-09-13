using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using UniRx.Triggers;

public class MainScene : SceneBase
{
    [SerializeField] float playTime = 60f;

    static System.Nullable<LevelDesignManager.RandomTable> table;
    static int index = -1;

    LevelDesignManager lv;

    private void Awake()
    {
        MainManager.ChangeState(MainManager.GameState.GAME_INIT);

        MainManager.OnStateChanged.Subscribe(s =>
        {
            switch (s)
            {
                case MainManager.GameState.GAME_PLAYING:
                    {
                        Observable.Timer(System.TimeSpan.FromSeconds(playTime)).Subscribe(_ => MainManager.ChangeState(MainManager.GameState.GAME_TIMEUP));
                    }
                    break;
                case MainManager.GameState.GAME_TIMEUP:
                    {
                        ResultManager result = GameObject.FindObjectOfType<ResultManager>();
                        if (result)
                        {
                            result.ComputeScore();
                        }
                    }
                    break;
            }
        }).AddTo(this);

        if (table == null)
        {
            lv = GameObject.FindObjectOfType<LevelDesignManager>();
            if (lv)
            {
                table = lv.RandomPickTable();
            }
		}
    }

    void Start () 
    {
        InitLevelSetting();
        InitPlayerPosition();
        MainManager.ChangeState(MainManager.GameState.GAME_NETWORK);

#if UNITY_EDITOR
        MainManager mm = GameObject.FindObjectOfType<MainManager>();
        if (!mm)
        {
            Observable.Timer(System.TimeSpan.FromSeconds(1)).Subscribe(_ =>
            {
                MainManager.ChangeState(MainManager.GameState.GAME_START);
                NetworkObject[] networks = GameObject.FindObjectsOfType<NetworkObject>();
                networks.ToList().ForEach(g => g.enabled = true);
            });
        }

        this.UpdateAsObservable().Subscribe(_ =>
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (MainManager.CurrentState == MainManager.GameState.GAME_START ||
                    MainManager.CurrentState == MainManager.GameState.GAME_NETWORK ||
                    MainManager.CurrentState == MainManager.GameState.GAME_PLAYING ||
                    MainManager.CurrentState == MainManager.GameState.GAME_RESULT)
                {
                    MainManager.ChangeState(MainManager.GameState.GAME_FINISH);
                    MainManager.LoadSceneAsync(sceneName);
                }
            }
        });
#endif

    }

    void InitLevelSetting()
    {
        if (!lv)
        {
            lv = GameObject.FindObjectOfType<LevelDesignManager>();
        }

        if (lv)
        {
            index = ++index % table.Value.Sequence.Length;
            System.Nullable<LevelDesignManager.LevelSetting> setting = lv.GetLevelSetting(table.Value.Sequence[index]);
            if (setting != null)
            {
                setting.Value.GeneratorSetting.gameObject.SetActive(true);
                startPosition = setting.Value.StartPositionSet.ToList();
            }
        }
    }
}
