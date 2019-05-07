using UnityEngine.Audio;
using UnityEngine;

// Mark it as Serializable, so that we can it in inspector
[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;

    [HideInInspector]
    public AudioSource audioSource;

    public bool loop; 
}
