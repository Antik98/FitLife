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
    }

    public void Reset()
    {
        GetComponent<QuestTracker>()?.Reset();
        GetComponent<GameTimer>()?.Reset();
        GetComponent<PlayerStatus>()?.Reset();
        coroutineQueue?.Reset();
        interactionTracker?.Reset();
    }
    public void Stop()
    {
        gameTimer.StopTimer();
    }
}