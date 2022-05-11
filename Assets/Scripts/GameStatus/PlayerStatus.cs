using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatus : MonoBehaviour, IStatusControllerService
{
    public enum Stats
    {
        ENERGY = 0,
        HUNGER = 1,
        SOCIAL = 2
    }

    public int energy { get; private set; } = 100; 
    public int social { get; private set; } = 50;
    public int hunger { get; private set; } = 100;
    public bool doTutorial = false;
    private GameTimer gameTimer;
    public static readonly int minutesUntilStatDecrease = 11;
    private int minuteCounter = 0;

    public delegate void AttributesChanged();
    public event AttributesChanged HandleAttributesChanged;

    public delegate void EventTriggeredAttributeChanged(object sender, Stats stats, int value);
    public event EventTriggeredAttributeChanged HandleEventTriggeredAttributeChanged;
    
    public void Reset()
    {
        StopAllCoroutines();
        energy = 100;
        social = 50;
        hunger = 100;
        Start();
    }

    public int LimitToRange(int value, int inclusiveMinimum, int inclusiveMaximum)
    {
        return Mathf.Clamp(value, inclusiveMinimum, inclusiveMaximum);
    }

    private void Start()
    {
        gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        //StartCoroutine(infLoop());
    }
    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());
    }
    private IEnumerator OnEnableCoroutine()
    {
        yield return new WaitUntil(() => StatusController.initialized);
        StatusController.Instance.gameTimer.BroadcastMinutePassed += HandleMinuteChanged;
    }
    private void OnDisable()
    {
        StatusController.Instance.gameTimer.BroadcastMinutePassed -= HandleMinuteChanged;
    }

    private void HandleMinuteChanged()
    {
        if(++minuteCounter == minutesUntilStatDecrease)
        {
            minuteCounter = 0;

            if (energy <= 0 || hunger <= 0 || social <= 0)
            {
                gameTimer.StopTimer();
                lowStatEvent();
                return;
            }

            hunger -= 1;
            energy -= 1;
            HandleAttributesChanged?.Invoke();
        }
    }


    private void lowStatEvent()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneController>().LoadScene("endingScene");
    }

    //IEnumerator infLoop()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(8f);
    //        Debug.Log("Stat change");
    //        if (!gameTimer.TimerStoped())
    //        {

    //            if (energy <= 0 || hunger <= 0 || social <= 0)
    //            {
    //                gameTimer.StopTimer();
    //                lowStatEvent();
    //                yield break;
    //            }
               
    //            hunger -= 1;
    //            energy -= 1;
    //            HandleAttributesChanged?.Invoke();
    //        }
    //    }
    //}

    public void addStatValues(int energyVal = 0, int socialVal = 0, int hungerVal = 0)
    {
        StartCoroutine(addValues(energyVal, socialVal, hungerVal));
    }

    private IEnumerator addValues(int energyVal = 0, int socialVal = 0, int hungerVal = 0)
    {
        energy = LimitToRange(energyVal + energy, 0, 100);
        social = LimitToRange(socialVal + social, 0, 100);
        hunger = LimitToRange(hungerVal + hunger, 0, 100);
        if (energyVal != 0)
        {
            HandleEventTriggeredAttributeChanged?.Invoke(this, Stats.ENERGY, energyVal);
            yield return new WaitForSeconds(2);
        }
        if (socialVal != 0)
        {
            HandleEventTriggeredAttributeChanged?.Invoke(this, Stats.SOCIAL, socialVal);
            yield return new WaitForSeconds(2);
        }
        if(hungerVal != 0)
        {
            HandleEventTriggeredAttributeChanged?.Invoke(this, Stats.HUNGER, hungerVal);
            yield return new WaitForSeconds(2);
        }
        yield return null;
    }

}
