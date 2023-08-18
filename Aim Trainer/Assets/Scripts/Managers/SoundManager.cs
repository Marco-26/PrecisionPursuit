using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class SoundManager : MonoBehaviour{
    public static SoundManager Instance;

    private AudioSource _audioSource;
    private Dictionary<Sound, AudioClip> soundAudioClipDictionary;

    public enum Sound {
        WeaponShoot,
        ObstacleHit,
        MenuButtonHover
    }

    private void Awake() {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();

        soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();

        foreach(Sound sound in System.Enum.GetValues(typeof(Sound))) {
            Debug.Log(sound.ToString());
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sound sound) {
        _audioSource.volume = 0.1f;
        _audioSource.PlayOneShot(soundAudioClipDictionary[sound]);
    }

    public void PlayButtonHoverSound() {
        _audioSource.volume = 1.0f;
        _audioSource.PlayOneShot(soundAudioClipDictionary[Sound.MenuButtonHover]);
    }
}
