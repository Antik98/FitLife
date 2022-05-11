using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour, IStatusControllerService
{
    public AudioClip[] Music;
    public Sound[] sounds;
    public AudioSource musicSource;
    private float mainMusicPosition;

    private void Start()
    {
        //audioSource.time += Random.Range(0, audioSource.clip.length); // random start
        updateMusic(SceneManager.GetActiveScene().name);
    }
    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());
    }

    private IEnumerator OnEnableCoroutine()
    {
        yield return new WaitUntil(() => StatusController.initialized);
        StatusController.Instance.coroutineQueue.OnSceneChange += updateMusic;
        foreach(Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;

            s.audioSource.pitch = s.pitch;
            s.audioSource.volume = s.volume;
            s.audioSource.loop = s.loop;
        }
    }
    private void OnDisable()
    {
        StatusController.Instance.coroutineQueue.OnSceneChange -= updateMusic;
    }

    public void restartMusic()
    {
        musicSource.time = 0;
    }
    public void Reset()
    {
        restartMusic();
    }

    public void playSoundName(string name, bool play = true)
    {
        Sound s = Array.Find(sounds, x => x.name == name);
        if(s == null)
        {
            Debug.LogWarning($"Sound {name} not found");
            return;
        }
        if (play)
            s.audioSource.Play();
        else
            s.audioSource.Stop();
    }

    public void playMusicIndex(int num)
    {
        if (musicSource.clip == Music[0])
            mainMusicPosition = musicSource.time;

        musicSource.Stop();
        musicSource.clip = Music[num];
        musicSource.Play();

        if (num == 0)
            musicSource.time = mainMusicPosition;
    }

    public bool updateMusic(string currentScene)
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "NTK")
        {
            if(musicSource.clip != Music[1])
            {
                playMusicIndex(1);
            }
        }
        else if (currentScene == "NTK_game")
        {
            if (musicSource.clip != Music[2])
            {
                playMusicIndex(2);
            }
        }
        else if (currentScene == "CAOPrednaska" || currentScene == "MLO_zkouska" || currentScene == "MLOCviko" || currentScene == "PA1Proseminar" || currentScene == "PAI_test" || currentScene == "PS1_test" || currentScene == "ZMASeminar")
        {
            if (musicSource.clip != Music[3])
            {
                playMusicIndex(3);
            }
        }
        else if (currentScene == "CAO_MinigameWinningScreen" || currentScene == "CAO_MinigameStartScreen" || currentScene == "CAO_MinigameLooseScreen" || currentScene == "CAO_Minigame")
        {
            if (musicSource.clip != Music[4])
            {
                playMusicIndex(4);
            }
        }
        else if (currentScene == "ZMA_MinigameWinningScene" || currentScene == "ZMA_MinigameStartScreen" || currentScene == "ZMA_MinigameLooseScreen" || currentScene == "ZMA_Minigame 1" || currentScene == "endingScene")
        {
            if (musicSource.clip != Music[5])
            {
                playMusicIndex(5);
            }
        }

        else if (currentScene == "PubScene" || currentScene == "Pub2ndFloorScene" || currentScene == "LastScreen" || currentScene == "VyherniObrazovka" || currentScene == "VyherniObrazovka2")
        {
            if (musicSource.clip != Music[6])
            {
                playMusicIndex(6);
            }
        }
        else
        {
            if(musicSource.clip != Music[0])
            {
                playMusicIndex(0);
            }
        }
        return true;
    }
}
