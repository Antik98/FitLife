using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PcInteract : DisplayHint
{
    private PlayerStatus playerStaus;
    private GameTimer gameTimer;
    private bool used = false;
    public int cd = 5;
    public int useValue = 10;
    private int timer = 0;
    private string displayTextOnHint = "uč se zmáčknutím e \n ( + 10 study ) ";
    

    void Start()
    {
        playerStaus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
        gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        labelText = displayTextOnHint;
    }

    public override void Action()
    {
        if (HasCollided() && Input.GetKeyDown("e") && !used)
        {
            used = true;
            gameTimer.gameTime += TimeSpan.FromMinutes(cd);
        }
    }

}
