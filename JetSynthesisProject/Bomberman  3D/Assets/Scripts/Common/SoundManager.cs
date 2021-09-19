using System;
using UnityEngine;

/// <summary>
/// This is singleton class handles all sounds in the game
/// </summary>

namespace JetSynthesis.BomberMan3D
{
    public class SoundManager : MonoBehaviour
    {
        [Header("Sound Settings")]
        private static SoundManager instance;
        public static SoundManager Instance { get { return instance; } }
        [SerializeField] private AudioSource soundEffect;
        [SerializeField] private AudioSource soundMusic;
        [SerializeField] private SoundType[] Sounds;
        public bool isMute = false;
        public float volume = 1f;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // In Start method, playing BG music from start with PlayMusic derived method
        private void Start()
        {
            SetVolume(volume / 2);
            PlayMusic(global::Sounds.Music);
        }

        // In this method, checking status is it mute or not
        public void Mute(bool status)
        {
            isMute = status;
        }

        // In this method, setting the volume
        public void SetVolume(float volume)
        {
            this.volume = volume;
            soundEffect.volume = volume;
            soundMusic.volume = volume;
        }

        // In PlayMusic method, getting sound clip check for null and calls Play() built in unity method
        public void PlayMusic(Sounds sound)
        {
            if (isMute)
            {
                return;
            }

            AudioClip clip = getSoundClip(sound);
            if (clip != null)
            {
                soundMusic.clip = clip;
                soundMusic.Play();
            }
            else
            {
                Debug.LogError("Clip not found for sound type: " + sound);
            }
        }

        // In Play method, getting sound clip check for null and play with PlayOneShot() built in unity method
        public void Play(Sounds sound)
        {
            if (isMute)
            {
                return;
            }

            AudioClip clip = getSoundClip(sound);
            if (clip != null)
            {
                soundEffect.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError("Clip not found for sound type: " + sound);
            }
        }

        // In getSoundClip method, getting soundtypes in array and check for required sound and returns, else returns null
        private AudioClip getSoundClip(Sounds sound)
        {
            SoundType item = Array.Find(Sounds, i => i.soundType == sound);
            if (item != null)
            {
                return item.soundClip;
            }
            return null;
        }
    }
}

/// <summary>
/// using SoundType Serializable class, for getting soundtype and sound clip from unity UI
/// </summary>

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
}


/// <summary>
/// Using sounds enum for different music types
/// </summary>

public enum Sounds
{
    buttonClick,
    Music,
    LevelWin,
    LevelLose,
    Explosion,
}