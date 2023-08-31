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

    private float defaultAudioVolume = 0.5f;

    private void Awake() {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = defaultAudioVolume;

        soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();

        foreach(Sound sound in System.Enum.GetValues(typeof(Sound))) {
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sound sound) {
        _audioSource.PlayOneShot(soundAudioClipDictionary[sound]);
    }

    public void PlayButtonHoverSound() {
        _audioSource.PlayOneShot(soundAudioClipDictionary[Sound.MenuButtonHover]);
    }

    public void ChangeVolume(float audioLevel) {
        _audioSource.volume = audioLevel;
    }

    public void MuteAudio()
    {
        _audioSource.volume = 0.0f;
    }
}
