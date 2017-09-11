using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

//Inspectorに二重で配列を表示するために使用
[System.Serializable]
public struct WaveData {
    public bool Tem_Sce_WarpViewOnTime;             //時間経過での出現かどうか
    public float Tem_Sce_TimeOnWarpView;            //経過時間の基準
    public bool Tem_Num_WarpViewOnGemNum;           //ジェムのトータル数に応じたシーン移行
    public int Tem_Num_GemOnWarpView;               //いくつで移行するか

    public int Tem_WarpWave;                        //移行先のウェーブ

    public bool Get_Sce_OnTimeNextView;             //時間経過で次のシーンへ
    public float Get_Sce_TimeNextView;              //時間指定へ
    public bool Get_Num_OnGemNumNextView;           //トータル数で次のシーンへ
    public int Get_Num_GemNumNextView;              //必要ジェム総数　次のシーンに移行

    public bool IsAddMode;                          //以前のジェムを消すか消さないか
}


public class GaneratorController : MonoBehaviour {
    [SerializeField] GemGanerator m_gemGanerator;
    [SerializeField] EnemyGanrator m_enemyGanerator;
    [SerializeField] List<WaveData> m_WaveList;

    private int m_totalGem;                         //ゲーム中のGem取得総数
    private int m_wave_totalGem;                    //Wave中のGem取得総数　別にしないとWave
    [SerializeField]
    private int m_nowWaveNum;
    private List<PlayerManager> m_PlayerManagers; 
    [SerializeField]
    private float m_totalTime;

    //一度消すためにジェムを管理させる
    [SerializeField]
    private List<GameObject> m_ControllGemList;

    private void Awake( ) {
        for( int i = 0; i < m_ControllGemList.Count; i++ ) {
            m_ControllGemList[ i ].gameObject.SetActive( false );
        }
    }

    private void Start( ) {
        m_totalGem = 0;
        m_nowWaveNum = 0;
        MainManager.OnEventHappaned.Subscribe(e =>
        {
            if (e.gameEvent == MainManager.GameEvent.EVENT_GEM) {
                IsGetGem();
            }
        }).AddTo(this);
    }

    public void IsGetGem( ) {
        m_wave_totalGem++;
    }

    private void Update( ) {
        MainManager.GameState game_state = MainManager.CurrentState;
        UpdateWave( );

        switch (game_state) {
            case MainManager.GameState.GAME_START:
                m_totalTime = 0;
                break;
            case MainManager.GameState.GAME_PLAYING:
                m_totalTime += Time.deltaTime;
                UpdateWave();
                break;
            case MainManager.GameState.GAME_FINISH:
                int i = 0;
                break;
            default:
                break;
        }
    }

    private void UpdateWave() {
        //ジェムの数によるシーン移行
        if ( m_WaveList[m_nowWaveNum].Tem_Num_WarpViewOnGemNum ) {
            if ( m_wave_totalGem >= m_WaveList[m_nowWaveNum].Tem_Num_GemOnWarpView) {
                m_nowWaveNum = m_WaveList[m_nowWaveNum].Tem_WarpWave;
                m_gemGanerator.SetWave(m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum);
                m_enemyGanerator.SetWave( m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum );
                m_totalTime = 0;
                AddWaveGemToTotal( m_wave_totalGem );
                m_wave_totalGem = 0;
                return;
            }
        }

        //時間経過によるウェーブ移行
        if ( m_WaveList[m_nowWaveNum].Tem_Sce_WarpViewOnTime ) {
            if ( m_totalTime >= m_WaveList[m_nowWaveNum].Tem_Sce_TimeOnWarpView ) {
                m_nowWaveNum = m_WaveList[m_nowWaveNum].Tem_WarpWave;
                m_gemGanerator.SetWave(m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum);
                m_enemyGanerator.SetWave( m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum );
                m_totalTime = 0;
                AddWaveGemToTotal( m_wave_totalGem );
                m_wave_totalGem = 0;
                return;
            }
        }


        if ( m_totalTime >= m_WaveList[m_nowWaveNum].Get_Sce_TimeNextView && m_WaveList[m_nowWaveNum].Get_Sce_OnTimeNextView ) {
            m_nowWaveNum++;
            m_gemGanerator.SetWave(m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum);
            m_enemyGanerator.SetWave( m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum );
            m_totalTime = 0;
            AddWaveGemToTotal( m_wave_totalGem );
            m_wave_totalGem = 0;
            return;
        }


        if ( m_wave_totalGem >= m_WaveList[m_nowWaveNum].Get_Num_GemNumNextView && m_WaveList[m_nowWaveNum].Get_Num_OnGemNumNextView) {
            m_nowWaveNum++;
            m_gemGanerator.SetWave(m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum);
            m_enemyGanerator.SetWave( m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum );
            m_totalTime = 0;
            AddWaveGemToTotal( m_wave_totalGem );
            m_wave_totalGem = 0;
            return;
        }
    }

    private void AddWaveGemToTotal( int wave_num ) {
        m_totalGem += wave_num;
    }
    
}
