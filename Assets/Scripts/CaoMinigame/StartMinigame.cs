﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMinigame : MonoBehaviour
{
    private GameTimer gameTimer;
    private void Start()
    {
        gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        gameTimer.StopTimer();
    }
    void Update()
    {
        if ( Input.GetKeyDown("e") )
            SceneManager.LoadScene( "CAO_Minigame" );
    }
}