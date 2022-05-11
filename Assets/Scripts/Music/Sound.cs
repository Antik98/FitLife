using UnityEngine.Audio;
using UnityEngine;

// This code is from Brackeys youtube channel https://www.youtube.com/watch?v=6OT43pvUyfY
[System.Serializable]
public class Sound
{
    public string name;

    [HideInInspector]
    public AudioSource audioSource;

    public AudioClip audioClip;

    public bool loop;

    [Range(0f, 1f)]
    public float volume;
    [Range(.3f,3f)]
    public float pitch;
}
