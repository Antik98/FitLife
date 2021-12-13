using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    public int energy = 100; 
    public int social = 50;
    public int hunger = 100;
    public int foundEasterEggs = 0;
    private GameTimer gameTimer;

    public void Reset()
    {
        StopAllCoroutines();
        energy = 100;
        social = 50;
        hunger = 100;
        foundEasterEggs = 0;
        Start();
    }

    public int LimitToRange(int value, int inclusiveMinimum, int inclusiveMaximum)
    {
        if (value < inclusiveMinimum) { return inclusiveMinimum; }
        if (value > inclusiveMaximum) { return inclusiveMaximum; }
        return value;
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
            yield return new WaitForSeconds(15f);
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
            }
        }
    }


    
    public void addEnergyValue(int value)
    {
        energy += value;
        LimitToRange(energy, 0, 100);
    }

    public void addSocialValue(int value)
    {
        social += value;
        LimitToRange(social, 0, 100);
    }

    public void addHungerValue(int value)
    {
        hunger += value;
        LimitToRange(hunger, 0, 100);
    }

}
