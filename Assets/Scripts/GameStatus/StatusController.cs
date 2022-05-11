using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    public QuestTracker questTracker;
    public GameTimer gameTimer;
    public PlayerStatus PlayerStatus;
    public InteractionTracker interactionTracker;
    public CoroutineQueue coroutineQueue;
    public AudioSource musicSource;
    public AudioManager audioManager;

    public static bool initialized => _instance != null;

    private static StatusController _instance;

    public static StatusController Instance { get { return _instance; } }

    void Awake()
    {
        if ( _instance != null)
        {
            Destroy(gameObject);
            return;
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
            Stop();
        _instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
        questTracker = GetComponent<QuestTracker>();
        gameTimer = GetComponent<GameTimer>();
        PlayerStatus = GetComponent<PlayerStatus>();
        interactionTracker = GetComponent<InteractionTracker>();
        coroutineQueue = GetComponent<CoroutineQueue>();
        musicSource = GetComponent<AudioSource>();
        audioManager = GetComponent<AudioManager>();
    }

    public void Reset()
    {
        Array.ForEach(GetComponents<IStatusControllerService>(), s => s.Reset());
        musicSource.time = 0;
    }

    public void Stop()
    {
        gameTimer.StopTimer();
    }
}