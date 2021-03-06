﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspectorに二重で配列を表示するために使用
[System.Serializable]
public class EnmeyWaveList
{
    public List<int> m_List = new List<int>();

    public EnmeyWaveList(List<int> list) {
        m_List = list;
    }
}

public class EnemyGanrator : MonoBehaviour {
    //エネミーの加算方法
    public enum ADD_MODE {
        ADD,
        CHANGE,
        MODE_MAX
    }

    //一番外側のサイズはWave数を設定する
    //Element一つ一つの中のサイズが各Waveの敵の数をする
    //最後のElementは敵の番号を設定する
    [SerializeField]
    private List<EnmeyWaveList> m_EnemyWave = new List<EnmeyWaveList>();
    //敵のマネージャー
    [SerializeField]
    private CharaManager m_charaManager;
    [SerializeField]
    private int m_AddMode;

    //現在のWave数
    private int m_now_wave;

    public int NowWave {
        get {
            return m_now_wave;
        }
    }

    private void Start( ) {
        m_now_wave = 0;
        m_charaManager.SetAllEnemyActive(false);

    }

    private void Update() {
        MainManager.GameState game_state = MainManager.CurrentState;
        switch (game_state) {
            case MainManager.GameState.GAME_START:
                SetEnemyWaveActice(true);
                break;
            case MainManager.GameState.GAME_PLAYING:
                CheckWaveView();
                break;
            case MainManager.GameState.GAME_TIMEUP:
                m_charaManager.SetAllEnemyActive(false);
                break;
            default:
                break;
        }
        DebugCode();
    }
    private void DebugCode() {
        CheckWaveView();
    }
    private void CheckWaveView() {

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            m_AddMode++;
            m_AddMode = m_AddMode % (int)ADD_MODE.MODE_MAX;
        }

        if (!Input.GetKeyDown(KeyCode.N)) {
            return;
        }

        switch ((ADD_MODE)m_AddMode) {
            case ADD_MODE.ADD:
                m_now_wave++;
                SetEnemyWaveActice(true);
                break;
            case ADD_MODE.CHANGE:
                //前のものの表示を消す
                m_charaManager.SetAllEnemyActive(false);
                m_now_wave++;
                SetEnemyWaveActice(true);
                break;
        }
    }

    private void SetEnemyWaveActice(bool flg_act) {
        foreach (int index in m_EnemyWave[m_now_wave].m_List) {
            if (index > 0) {
                m_charaManager.SetEnemyActive(index - 1, flg_act);
            }
        }
    }

    public void NextWave(ADD_MODE mode) {
        switch (mode) {
            case ADD_MODE.ADD:
                m_now_wave++;
                SetEnemyWaveActice(true);
                break;
            case ADD_MODE.CHANGE:
                //前のものを表示を消す
                m_charaManager.SetAllEnemyActive(false);
                m_now_wave++;
                SetEnemyWaveActice(true);
                break;
        }
    }

    public void SetWave(bool isAddMode, int wave) {
        if (isAddMode) {
            m_now_wave = wave;
            SetEnemyWaveActice(true);
        } else {
            //前のものを表示を消す
            m_charaManager.SetAllEnemyActive(false);
            m_now_wave = wave;
            SetEnemyWaveActice(true);
        }
    }

}
