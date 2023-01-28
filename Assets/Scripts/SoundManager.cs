using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public List<Sound> sounds;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource secondAudioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(string soundName)
    {
        foreach (var sound in sounds)
        {
            if (sound.name == soundName)
            {
                if (!audioSource.isPlaying)
                {
                    AudioSourceHandler(audioSource, sound);
                }
                else
                {
                    AudioSourceHandler(secondAudioSource, sound);
                }
            }
        }
    }

    private void AudioSourceHandler(AudioSource audioSource, Sound sound)
    {
        audioSource.clip = sound.clip;
        audioSource.volume = sound.volume;
        audioSource.Play();
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(.1f, 1f)] public float volume;
}