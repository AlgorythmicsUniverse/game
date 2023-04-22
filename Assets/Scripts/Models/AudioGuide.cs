using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGuide : MonoBehaviour {

    public AudioClip audioClip;

    public void playAudio() {
        if (audioClip != null) {
            AudioController.Instance.play(audioClip);
        }
    }
}
