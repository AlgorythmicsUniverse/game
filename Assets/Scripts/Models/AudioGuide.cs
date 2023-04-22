using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGuide : MonoBehaviour {

    public AudioClip audioClip;
    private AudioController audioController;

    public void playAudio() {
        Debug.Log("play something, idk");
        // audioController.play(audioClip);
    }
}
