using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandSoundManager : MonoBehaviour {

    [SerializeField] private SoundDataComponent sound;
    private void Awake() {
        sound.soundData._sound.Play();
        sound.soundData._sound.Pause();
    }
    private void Update() {
        if (MainManager.CurrentState == MainManager.GameState.GAME_START) {
            sound.soundData._sound.UnPause();
        }
    }
}
