using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{
    public static SoundManager Instance;

    private AudioSource _audioSource;
    private Dictionary<Sound, AudioClip> soundAudioClipDictionary;

    public enum Sound {
        WeaponShoot,
        ObstacleHit
    }

    private void Awake() {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();

        soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();

        foreach(Sound sound in System.Enum.GetValues(typeof(Sound))) {
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sound sound) {
        _audioSource.PlayOneShot(soundAudioClipDictionary[sound]);
    }
}
