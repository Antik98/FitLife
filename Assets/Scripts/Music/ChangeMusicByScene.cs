using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicByScene : MonoBehaviour
{
    public AudioClip[] Music;
    AudioSource audioSource;
    private float mainMusicPosition;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.time += Random.Range(0, audioSource.clip.length); // random start
        spareTheDevs();
    }
    
    public void restartMusic()
    {
        audioSource.time = 0;
    }

    public void playMusicIndex(int num)
    {
        if (audioSource.clip == Music[0])
            mainMusicPosition = audioSource.time;

        audioSource.Stop();
        audioSource.clip = Music[num];
        audioSource.Play();

        if (num == 0)
            audioSource.time = mainMusicPosition;

        spareTheDevs();
    }

    private void spareTheDevs()
    {
        if (Application.isEditor && audioSource.clip == Music[0])
        {
            audioSource.Stop();
        }
    }

    public void updateMusic(string currentScene)
    {
        
        if (currentScene == "NTK")
        {
            if(audioSource.clip != Music[1])
            {
                playMusicIndex(1);
            }
        }
        else if (currentScene == "NTK_game")
        {
            if (audioSource.clip != Music[2])
            {
                playMusicIndex(2);
            }
        }
        else
        {
            if(audioSource.clip != Music[0])
            {
                playMusicIndex(0);
            }
        }
    }
}
