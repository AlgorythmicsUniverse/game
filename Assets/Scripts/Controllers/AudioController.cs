using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource EffectsSource;
    public AudioSource MusicSource;

    public float LowPitchRange = 0.95f;
    public float HighPitchRange = 1.05f;

    public static AudioController Instance = null;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void play(AudioClip clip) {
        EffectsSource.clip = clip;
        EffectsSource.Play();
    }

    public void playMusic(AudioClip clip) {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    public void randomSoundEffect(params AudioClip[] clips) {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

        EffectsSource.pitch = randomPitch;
        EffectsSource.clip = clips[randomIndex];
        EffectsSource.Play();
    }
}
