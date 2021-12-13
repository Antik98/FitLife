using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatDisplay : MonoBehaviour
{
    private PlayerStatus playerStatus;
    private GameTimer gameTimer;
    bool display = false;

    public Text time;   
    public Text hunger;
    public Text energy;
    public Text social;


    // Start is called before the first frame update
    void Start()
    {
        var statusController = GameObject.FindGameObjectWithTag("StatusController");
        if (statusController ?? false)
        {
            playerStatus = statusController.GetComponent<PlayerStatus>();
            gameTimer = statusController.GetComponent<GameTimer>();
            display = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (display)
        {
            DisplayTime();
            DisplayHunger();
            DisplayEnergy();
            DisplaySocial();
        }
    }
    
    void DisplayTime()
    {
        time.text = gameTimer.GetCurrentTimeString();

    }

    void DisplayHunger()
    {
        hunger.text = playerStatus.hunger.ToString();
    }

    void DisplayEnergy()
    {
        energy.text = playerStatus.energy.ToString();
    }

    void DisplaySocial()
    {
        social.text = playerStatus.social.ToString();
    }

}
