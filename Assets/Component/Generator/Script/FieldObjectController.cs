﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObjectController : MonoBehaviour {

    [SerializeField] SoundDataComponent sound;

    public void SetSoundActive( bool active ) {
        if (active) {
            sound.soundData._sound.UnPause();
        } else { 
            sound.soundData._sound.Pause();
        }
    }
}
