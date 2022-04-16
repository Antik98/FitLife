using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatus : MonoBehaviour
{
    public enum Stats
    {
        ENERGY = 0,
        HUNGER = 1,
        SOCIAL = 2
    }

    public int energy = 100; 
    public int social = 50;
    public int hunger = 100;
    public bool doTutorial = false;
    private GameTimer gameTimer;

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
        StartCoroutine(infLoop());
    }


    private void lowStatEvent()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneController>().LoadScene("endingScene");
    }

    IEnumerator infLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(12f);
            if (!gameTimer.TimerStoped())
            {

                if (energy <= 0 || hunger <= 0 || social <= 0)
                {
                    gameTimer.StopTimer();
                    lowStatEvent();
                    yield break;
                }
               
                hunger -= 1;
                energy -= 1;


                if (social >= 100)
                {
                    social = 100;
                }
                // social -= 1;
                HandleAttributesChanged?.Invoke();
            }
        }
    }

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
