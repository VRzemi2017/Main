using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using UniRx.Triggers;

public class MainScene : SceneBase
{
    static System.Nullable<LevelDesignManager.RandomTable> table;
    static int index = -1;

    LevelDesignManager lv;

    private void Awake()
    {
        MainManager.ChangeState(MainManager.GameState.GAME_INIT);

        if (table == null)
        {
            lv = GameObject.FindObjectOfType<LevelDesignManager>();
            if (lv)
            {
                table = lv.RandomPickTable();
            }
        }

        GemGanerator[] gems = GameObject.FindObjectsOfType<GemGanerator>();
        gems.ToList().ForEach(g => g.gameObject.SetActive(false));

        EnemyGanrator[] enemies = GameObject.FindObjectsOfType<EnemyGanrator>();
        enemies.ToList().ForEach(e => e.gameObject.SetActive(false));
    }

    void Start () 
    {
        InitLevelSetting();
        InitPlayerPosition();
        MainManager.ChangeState(MainManager.GameState.GAME_NETWORK);

        this.UpdateAsObservable().Subscribe(_ =>
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (MainManager.CurrentState == MainManager.GameState.GAME_START ||
                    MainManager.CurrentState == MainManager.GameState.GAME_NETWORK ||
                    MainManager.CurrentState == MainManager.GameState.GAME_PLAYING ||
                    MainManager.CurrentState == MainManager.GameState.GAME_RESULT)
                {
                    MainManager.LoadSceneAsync(sceneName);
                    MainManager.ChangeState(MainManager.GameState.GAME_FINISH);
                }
            }
        });
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
                setting.Value.GemSetting.gameObject.SetActive(true);
                setting.Value.EnemySetting.gameObject.SetActive(true);
                startPosition = setting.Value.StartPositionSet.ToList();
            }
        }
    }
}
