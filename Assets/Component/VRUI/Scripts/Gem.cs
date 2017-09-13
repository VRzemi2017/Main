using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gem : MonoBehaviour {

    public GameObject GemEffect;

    private bool IsEndAnimation = true;
    private bool IsHitMy;
    public float HitGemTime { get; set; }
    public bool Is_End_Animation { get { return IsEndAnimation; } }
    [SerializeField]
    float Get_time = 1f;
    [SerializeField]
    AudioClip GemGetAudio;
    [SerializeField]
    AudioClip GemEffectAudio;


    bool IsGet = false;
    bool Gem_Ain_Controller = false;
    public bool Gem_Effect_TimeFlag = false;

    Action<GameObject> GetAction;
    private float Gem_Effect_Time = 0;
    private AudioSource audiosource;

    private FieldObjectController m_FieldObjectCont;
	// Use this for initialization
	void Start () {
        m_FieldObjectCont = gameObject.GetComponent<FieldObjectController>();
        audiosource = gameObject.GetComponent<AudioSource>( );
        IsGet = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (IsGet) {
            return;
        }

        if(IsHitMy) {
            Gem_Effect_Time += Time.deltaTime;

            if (Gem_Effect_Time >= Get_time) {
                GetAction(gameObject);
                IsHitMy = false;
                GemEffect.SetActive(false);
                IsGet = true;
                gameObject.tag = "Player";
                gameObject.layer = 8;
                Gem_Effect_Time = 0f;
                audiosource.clip = GemGetAudio;
                if ( audiosource != null ) {
                    audiosource.Play( );
                }
            }
        } else if ( Gem_Effect_Time > 0.0f ) {
            Gem_Effect_Time -= Time.deltaTime;
            if ( audiosource == null ) {
                audiosource.Stop( );
            }
            if (Gem_Effect_Time < 0.0f) {
                GemEffect.SetActive( false );
                Gem_Effect_Time = 0f;
            }
        }

       
        
	}

    public void SetSoundActiveFalse() {
        m_FieldObjectCont.SetSoundActive(false);
    }
    public void HitByPlayer(Action<GameObject> action) {
        if (IsGet) {
            return;
        }
        IsHitMy = true;
        GetAction = action;
        GemEffect.SetActive(true);
        GemEffect.GetComponent<Animator>().SetFloat("Speed", 1);

        audiosource.clip = GemEffectAudio;
        if ( audiosource != null ) {
            audiosource.Play( );
        }
    }

    public void HitPlayerLeave() {
        if (IsGet) {
            return;
        }
        IsHitMy = false;
        GemEffect.GetComponent<Animator>().SetFloat("Speed", -1);

        audiosource.clip = null;
        if ( audiosource == null ) {
            audiosource.Stop( );
        }

    }

    public void SetGetAction(  ) {
        
    }
}
