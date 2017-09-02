using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspectorに二重で配列を表示するために使用
[System.Serializable]
public class ObjectList {
    //配置しやすいように空のGameObjectにしてあるがVector3でも可
    public List<GameObject> m_List = new List<GameObject>( );

    public ObjectList( List<GameObject> list ) {
        m_List = list;
    }
}


public class GemGanerator : MonoBehaviour {

    //ジェムの加算方法
    public enum ADD_MODE {
        ADD,
        CHANGE,
        MODE_MAX
    }

    //--------Inspectorに表示される変数--------//
    //Gemを生成する場所の配列
    [SerializeField]
    private List<ObjectList> m_GemWaveList = new List<ObjectList>( );
    //Gemが生成される場所
    [SerializeField]
    private Transform m_GemParents;
    [SerializeField]
    private int m_AddMode;
    //---------------------------------------//
    
    //--------メンバ変数--------//
    //生成されるGemの配列番号
    private int m_gem_wave_num;
    //-------------------------//

    public int NowWave {
        get {
            return m_gem_wave_num;
        }
    }

    void Awake( ) {
    }

	void Start ( ) {
        m_gem_wave_num = 0;


        for (int i = 0; i < m_GemWaveList.Count; i++) {
            for ( int j = 0; j < m_GemWaveList[ i ].m_List.Count; j++ ) {
                m_GemWaveList[ i ].m_List[ j ].SetActive( false );
            }
        }
        //表示するべきGemをActive化させる
        SetGemWaveActive( true );

    }
	
	void Update ( ) {
		MainManager.GameState game_state = MainManager.CurrentState;
        GenerateUpdate();

        switch ( game_state ) {
            case MainManager.GameState.GAME_START:
                break;
            case MainManager.GameState.GAME_PLAYING:
                GenerateUpdate( );
                break;
            case MainManager.GameState.GAME_FINISH:
                m_GemParents.gameObject.SetActive(false);
                break;
            default:
                break;
        }
	}

    //Gem生成の為の更新
    private void GenerateUpdate( ) {

        if (Input.GetKeyDown(KeyCode.UpArrow) ) {
            m_AddMode++;
            m_AddMode = m_AddMode % ( int )ADD_MODE.MODE_MAX;
        }

        if ( !Input.GetKeyDown( KeyCode.A ) ) {
            return;
        }

        switch ( ( ADD_MODE )m_AddMode ) {
            case ADD_MODE.ADD:
                NextGems( );
                SetGemWaveActive(true);
                break;
            case ADD_MODE.CHANGE:
                //前のものを表示を消す
                ResetAllWave();
                NextGems( );
                SetGemWaveActive(true);
                break;
        }
    }



    //GemParentの更新
    private void NextGems( ) {
        m_gem_wave_num++;
    }

    private void SetGemWaveActive( bool flg_act ) {
        for (int i = 0; i < m_GemWaveList[m_gem_wave_num].m_List.Count; i++){
            m_GemWaveList[m_gem_wave_num].m_List[i].SetActive(flg_act);
            m_GemWaveList[m_gem_wave_num].m_List[i].GetComponent<FieldObjectController>().SetSoundActive(flg_act);
        }
    }

    private void ResetAllWave() {
        for (int i = 0; i < m_GemWaveList.Count; i++) {
            for (int j = 0; j < m_GemWaveList[i].m_List.Count; j++) {
                m_GemWaveList[i].m_List[j].SetActive(false);
                m_GemWaveList[m_gem_wave_num].m_List[i].GetComponent<FieldObjectController>().SetSoundActive(false);
            }
        }
    }

    public void NextWave(ADD_MODE mode) {
        switch (mode)　{
            case ADD_MODE.ADD:
                NextGems();
                SetGemWaveActive(true);
                break;
            case ADD_MODE.CHANGE:
                //前のものを表示を消す
                ResetAllWave();
                NextGems();
                SetGemWaveActive(true);
                break;
        }
    }

    public void SetWave(bool isAddMode, int wave){
        if (isAddMode) {
            m_gem_wave_num = wave;
            SetGemWaveActive(true);
        } else {
            //前のものを表示を消す
            ResetAllWave();
            m_gem_wave_num = wave;
            SetGemWaveActive(true);
        }
    }
}
