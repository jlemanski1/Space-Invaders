using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance = null;

    public AudioClip alienBuzz1;
    public AudioClip alienBuzz2;
    public AudioClip alienDeath;
    public AudioClip bulletFire;
    public AudioClip shipExplosion;

    private AudioSource soundEffectAudio;

    private void Start () {
        // Singleton Pattern
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        soundEffectAudio = GetComponent<AudioSource>();
        soundEffectAudio.volume = 0.1f;
	}
	
    // Play sound effect
	public void PlayOneShot(AudioClip clip) {
        soundEffectAudio.PlayOneShot(clip);
    }
}
