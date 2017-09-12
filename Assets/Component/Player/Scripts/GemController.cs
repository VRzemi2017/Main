using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour {
    [SerializeField] GameObject m_Gem;
    [SerializeField] GameObject m_SmallGemPrefab;
    [SerializeField] Transform m_SmallGemParent;
    [SerializeField] Vector3 m_SmallGemLenght;
    [SerializeField] GameObject m_LineRenderer;
    [SerializeField] float m_getGemAnimationTime;
    [SerializeField] GameObject m_hitAnimation;
    [SerializeField] AudioClip GemGetAudio;

    private bool m_is_hit_gem = false;
    public bool Hit_Gem { get { return m_is_hit_gem; } }

    private AudioSource audiosource;
    private bool m_is_game_start = false;
    private bool m_is_get_gem = false;
    private LineRendererController m_Line_render_cont;
    private Gem Gem_tauch_data;
    private float m_hit_gem_time = 0;
    //ヒットしたGemを一時的に保存する
    private GameObject m_hit_gem;

    private List<GameObject> m_SmallGemList = new List<GameObject>();
    
    public bool IsGetGem { get { return m_is_get_gem; } }
    public GameObject GetMyHitGem { get { return m_hit_gem; } }

    public void Start ( ) {
        m_Line_render_cont = m_LineRenderer.GetComponent<LineRendererController>( );
        audiosource = gameObject.GetComponent<AudioSource>();
    }

    public void Update ( ) {
        foreach ( GameObject gem in m_SmallGemList ) {
            gem.transform.RotateAround( m_SmallGemParent.position, m_SmallGemParent.forward, 1f );
        }

    }

    private void OnTriggerEnter(Collider collision) {
        if (!m_is_game_start) {
            return;
        }
        //ヒットした物の種類を取得するを取得する
        switch (collision.gameObject.tag){
            case "Gem":
                m_hit_gem = collision.gameObject;
                m_hit_gem.GetComponent<Gem>().HitByPlayer( _ => {
                    GetGemAction(_);
                });
                MainManager.EventData eventData;
                eventData.gameEvent = MainManager.GameEvent.EVENT_HIT_GEM;
                eventData.eventObject = m_hit_gem;
                MainManager.EventTriggered(eventData);
                m_Line_render_cont.ColorControllerOFF( );
                //audiosource.clip = GemGetAudio;
                //audiosource.Play();
                break;
            default:
                break;
        }
    }


    private void OnTriggerExit(Collider other) {
        if (!m_is_game_start) {
            return;
        }
        //ヒットした物の種類を取得するを取得する
        switch (other.gameObject.tag) {
            case "Gem":
                m_is_hit_gem = false;
                m_hit_gem.GetComponent<Gem>().HitPlayerLeave();
                MainManager.EventData eventData;
                eventData.gameEvent = MainManager.GameEvent.EVENT_LEAVR_GEM;
                eventData.eventObject = m_hit_gem;
                MainManager.EventTriggered(eventData);

                m_Line_render_cont.ColorControllerON( );
                audiosource.clip = GemGetAudio;
                audiosource.Stop();
                break;
            default:
                break;
        }

    }

    private void SetHitAnimationSpeed(float speed) {
        Animator anim = m_hit_gem.GetComponent<Gem>().GemEffect.GetComponent<Animator>();
        anim.SetFloat("Speed", speed);
    }

    public void GetGemAction(GameObject gem) {
        gem.transform.SetParent(m_SmallGemParent, false);
        m_SmallGemList.Add(gem);
        gem.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        gem.GetComponent<Gem>().SetSoundActiveFalse();
        ResetSmallGemPos();
        m_is_get_gem = true; 
        m_Line_render_cont.ColorControllerON();
    }

    private void ResetSmallGemPos() {
        for (int i = 0; i < m_SmallGemList.Count; i++) {
            Vector3 position = Quaternion.Euler(0, 0, (360 / m_SmallGemList.Count) * i) * m_SmallGemLenght;
            m_SmallGemList[i].transform.localPosition = position;
        }
    }

    public void ResetHitState() {
        m_is_get_gem = false;
    }

    public void SetGameStart(bool start) {
        m_is_game_start = start;
    }

}
