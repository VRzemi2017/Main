using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspectorに二重で配列を表示するために使用
[System.Serializable]
public struct WaveData {
    public bool Tem_Sce_WarpViewOnTime;
    public float Tem_Sce_TimeOnWarpView;
    public bool Tem_Num_WarpViewOnGemNum;
    public int Tem_Num_GemOnWarpView;
    public int Tem_WarpWave;
    public bool Get_Sce_OnTimeNextView;
    public float Get_Sce_TimeNextView;
    public bool Get_Num_OnGemNumNextView;
    public int Get_Num_GemNumNextView;
    public bool IsAddMode;

}


public class GaneratorController : MonoBehaviour {
    

    [SerializeField] GemGanerator m_gemGanerator;
    [SerializeField] EnemyGanrator m_enemyGanerator;
    [SerializeField] List<WaveData> m_WaveList;

    private int m_totalGem;
    private int m_nowWaveNum;
    private float m_totalTime;

    private void Start() {
        m_totalGem = 0;
        m_nowWaveNum = 0;
    }

    public void IsGetGem( ) {
        m_totalGem++;
    }

    private void Update() {
        MainManager.GameState game_state = MainManager.CurrentState;
        UpdateWave();

        switch (game_state) {
            case MainManager.GameState.GAME_START:
                m_totalTime = 0;
                break;
            case MainManager.GameState.GAME_PLAYING:
                m_totalTime += Time.deltaTime;
                UpdateWave();
                break;
            case MainManager.GameState.GAME_FINISH:
                break;
            default:
                break;
        }
    }

    private void UpdateWave() {
        if (m_WaveList[m_nowWaveNum].Tem_Num_WarpViewOnGemNum) {
            if (m_totalGem >= m_WaveList[m_nowWaveNum].Tem_Num_GemOnWarpView) {
                m_nowWaveNum = m_WaveList[m_nowWaveNum].Tem_WarpWave;
                m_gemGanerator.SetWave(m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum);
                return;
            }
        }

        if (m_WaveList[m_nowWaveNum].Tem_Sce_WarpViewOnTime) {
            if (m_totalTime >= m_WaveList[m_nowWaveNum].Tem_Sce_TimeOnWarpView) {
                m_nowWaveNum = m_WaveList[m_nowWaveNum].Tem_WarpWave;
                m_gemGanerator.SetWave(m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum);
                return;
            }
        }

        if (m_totalTime >= m_WaveList[m_nowWaveNum].Get_Sce_TimeNextView && m_WaveList[m_nowWaveNum].Get_Sce_OnTimeNextView) {
            m_nowWaveNum++;
            m_gemGanerator.SetWave(m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum);
            return;
        }

        if (m_totalGem >= m_WaveList[m_nowWaveNum].Get_Num_GemNumNextView && m_WaveList[m_nowWaveNum].Get_Num_OnGemNumNextView) {
            m_nowWaveNum++;
            m_gemGanerator.SetWave(m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum);
            return;
        }

    }
    
}
