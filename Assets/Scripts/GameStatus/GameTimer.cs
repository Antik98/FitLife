using System;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private SceneController sceneController;
    private QuestTracker questTracker;

    public const int  endingDay = 4; 
    public bool stoped =  false;

    [SerializeField]
    public TimeSpan gameTime;
    public  TimeSpan endingDayTime = new TimeSpan(endingDay, 0, 0, 0);
    public readonly TimeSpan initTime = new TimeSpan(0,6,0,0);

    public delegate void TimePeriodPassed();
    public event TimePeriodPassed BroadcastDayPassed;
    public event TimePeriodPassed Broadcast15MinutesPassed;

    private void Start()
    {
        stoped = false;
        gameTime = initTime;
        sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneController>();
        questTracker = StatusController.Instance.GetComponent<QuestTracker>();
    }

    public void Reset()
    {
        gameTime = initTime;
    }

    void Update()
    {
        if (!stoped)
        {
            InvokeTimePassedEvents();
            UpdateTime();
        }
    }

    // We can use these events to trigger quest timeouts, world events
    void InvokeTimePassedEvents() 
    {
        // Called when Days change
        if(gameTime.Days > (gameTime + TimeSpan.FromMilliseconds(Time.deltaTime)).Days)
        {
            gameTime += TimeSpan.FromHours(6f);
            BroadcastDayPassed?.Invoke();
        }

        //Called when 15 minutes passed
        if(gameTime.Minutes % 15 == 0)
        {
            Broadcast15MinutesPassed?.Invoke();
        }
       
    }


    private void UpdateTime()
    {
        if (!stoped)
        {   
            if (gameTime >= endingDayTime)
            {
                stoped = true; 
                EndGameSuccessfully();
            }

            gameTime += TimeSpan.FromSeconds(Time.deltaTime*60);
            
        }
    }

    private void EndGameSuccessfully()
    {
        sceneController.LoadScene("VyherniObrazovka");
    }

    public string GetCurrentTimeString()
    {
        return gameTime.Hours.ToString("00") + ":" + gameTime.Minutes.ToString("00");
    }

    public void StopTimer()
    {
        stoped = true;
    }

    public void StartTimer()
    {
        stoped = false;
    }

    public bool TimerStoped()
    {
        return stoped;
    }

    public void SleepHours(float hours)
    {
        gameTime += TimeSpan.FromHours(hours);
        Broadcast15MinutesPassed?.Invoke();
    }

    public void TriggerNextDay()
    {
        gameTime = new TimeSpan(gameTime.Days + 1, 0, 0, 1);
    }
}