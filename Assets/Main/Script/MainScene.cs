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

    public static MainScene Instance;

    private static bool useForceLevel = false;
    private static int forceLevel = 0;
    public static void ForceLevel(int lv)
    {
        forceLevel = lv;
        useForceLevel = true;

        Debug.Log("ForceLevel: " + lv);
    }

    LevelDesignManager lv;

    public int RemoteDamage { get; private set; }
    public int RemoteGem { get; private set; }
    public int SelfDamage { get; private set; }

    private void Awake()
    {
        Instance = this;

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
                            result.ComputeComment();
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
                if (MainManager.CurrentState == MainManager.GameState.GAME_RESULT)
                {
                    MainManager.LoadSceneAsync(sceneName);
                }
            }
        });

        MainManager.OnEventHappaned.Subscribe(e =>
        {
            switch (e.gameEvent)
            {
                case GameEvent.EVENT_DAMAGE:
                    {
                        if (MainManager.LocalPlayer == e.eventObject)
                        {
                            ++SelfDamage;
                        }
                        else
                        {
                            ++RemoteDamage;
                        }
                    }
                    break;
                case GameEvent.EVENT_GEM:
                    {
                        if (MainManager.RemoteWand && e.eventObject == MainManager.RemoteWand.gameObject)
                        {
                            ++RemoteGem;
                        }
                    }
                    break;
            }
        }).AddTo(this);
    }

    void InitLevelSetting()
    {
        if (!lv)
        {
            lv = GameObject.FindObjectOfType<LevelDesignManager>();
        }

        if (lv)
        {
            System.Nullable<LevelDesignManager.LevelSetting> setting = null;
            if (useForceLevel)
            {
                setting = lv.GetLevelSetting(forceLevel);
                useForceLevel = false;
            }
            else
            {
                index = ++index % table.Value.Sequence.Length;
                setting = lv.GetLevelSetting(table.Value.Sequence[index]);
            }
            if (setting != null)
            {
                setting.Value.GeneratorSetting.gameObject.SetActive(true);
                startPosition = setting.Value.StartPositionSet.ToList();
            }
        }
    }
}
