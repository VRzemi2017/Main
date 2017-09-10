using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

    private bool IsEndAnimation = true;
    public bool Is_End_Animation { get { return IsEndAnimation; } }
    private FieldObjectController m_FieldObjectCont;
	// Use this for initialization
	void Start () {
        m_FieldObjectCont = gameObject.GetComponent<FieldObjectController>();

    }
	
	// Update is called once per frame
	void Update () {

	}

    public void SetAnimationIsEnd( bool isEnd ) {
        IsEndAnimation = isEnd;
    }

    public void SetSoundActiveFalse() {
        m_FieldObjectCont.SetSoundActive(false);
    }
}
