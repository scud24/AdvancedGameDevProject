using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instane;
    //public List<AudioClip> soundClips;
    public AudioClip menuMusic;
    public AudioClip combatMusic;
    public string currentMusic;
    public AudioSource audioSource;
    void Awake()
    {
        if(instane == null) {
            instane = this;
        } else {
            Destroy(gameObject);
            return;
        }
        //source = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        
    }
    public void PlayMusic(int trackIndex)
    {
        //currentMusic = name;
        Play(trackIndex);
    }
    public void Play(int trackIndex) {
        Debug.Log("Track index " + trackIndex);
        //Debug.Log(soundClips[0]);
        audioSource.Stop();
        if (trackIndex == 0)
        {
            audioSource.PlayOneShot(menuMusic);
           // audioSource.clip = menuMusic;
        }
        else if(trackIndex == 1)
        {
            audioSource.PlayOneShot(combatMusic);
        }
        else
        {
            Debug.Log("Ping");
        }
        //audioSource = GetComponent<AudioSource>();
        
       // audioSource.Play();
    }
    public void StopMusic()
    {
        
    }
    public void StopAllSounds()
    {
        Debug.Log("stopSounds");
        
    }
}
