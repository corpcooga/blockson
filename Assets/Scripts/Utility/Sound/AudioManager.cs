using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioMixer gameplayMixer;
    public AudioMixer mainMixer
    {
        get
        {
            return gameplayMixer;
        }
    }

    bool check = false;
    public static AudioManager instance{get; private set;}
    public AudioMixerGroup audioMixer;

    public Sound[] sounds;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = audioMixer;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && !check)
        {
            Stop("Main Game Music");
            Play("Main Menu Music");
            check = true;
        } else if (SceneManager.GetActiveScene().buildIndex >= 1 && check)
        {
            Stop("Main Menu Music");
            Play("Main Game Music");
            check = false;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void SetGameplayVolume(float volume)
    {
        if (volume <= -30)
        {
            gameplayMixer.SetFloat("volume", -1000f);
        } else 
        {
            gameplayMixer.SetFloat("volume", volume);
        }
    }
}