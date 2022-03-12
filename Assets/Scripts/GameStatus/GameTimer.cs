using System;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private SceneController sceneController;

    public const int  endingDay = 3; 
    public bool stoped =  false;

    [SerializeField]
    public TimeSpan gameTime;
    public  TimeSpan endingDayTime = new TimeSpan(endingDay, 0, 0, 0);
    public readonly TimeSpan initTime = new TimeSpan(0,6,0,0);

    public delegate void TimePeriodPassed();
    public event TimePeriodPassed BroadcastDayPassed;
    public event TimePeriodPassed Broadcast15MinutesPassed;
    public event TimePeriodPassed BroadcastMinutePassed;

    private void Start()
    {
        stoped = false;
        gameTime = initTime;
        sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneController>();
    }

    public void Reset()
    {
        gameTime = initTime;
    }

    void Update()
    {
        if (Application.isEditor && (Input.GetKey(KeyCode.KeypadPlus) || Input.GetKey(KeyCode.Plus)))
        {
            gameTime += TimeSpan.FromMinutes(2);
            BroadcastMinutePassed?.Invoke();
        }
        if (!stoped)
        {
            InvokeTimePassedEvents();
            UpdateTime();
        }
    }

    // We can use these events to trigger quest timeouts, world events
    void InvokeTimePassedEvents() 
    {
        TimeSpan nextTick = gameTime + TimeSpan.FromSeconds(Time.deltaTime * 90);
        // Called when Days change
        if (gameTime.Days < nextTick.Days)
        {
            gameTime += initTime;
            BroadcastDayPassed?.Invoke();
        }

        //Called when 15 minutes passed
        if(gameTime.Minutes % 15 == 0 && nextTick.Minutes % 15 != 0 )
        {
            Broadcast15MinutesPassed?.Invoke();
        }

        //Called when 1 minute passed
        if(gameTime.Minutes != nextTick.Minutes)
        {
            BroadcastMinutePassed?.Invoke();
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

            gameTime += TimeSpan.FromSeconds(Time.deltaTime*90);
            
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
        if ((gameTime + TimeSpan.FromHours(hours)).Days > gameTime.Days)
            TriggerNextDay();
    }

    public void TriggerNextDay()
    {
        gameTime = new TimeSpan(gameTime.Days + 1, 0, 0, 1) + initTime;
        BroadcastMinutePassed?.Invoke();
        Broadcast15MinutesPassed?.Invoke();
        BroadcastDayPassed?.Invoke();
    }
}