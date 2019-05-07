using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instane;
    public Sound[] sounds;
    void Awake()
    {
        if(instane == null) {
            instane = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds) {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
        }
    }

    public void Play(string name) {
        Sound resultSound = Array.Find(sounds, sound => sound.name == name);
        if (resultSound == null) {
            Debug.LogWarning("Sound: "+name+" not found!");
        return;
         }

        resultSound.audioSource.Play();
    }
}
