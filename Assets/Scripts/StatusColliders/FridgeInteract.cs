﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FridgeInteract : DisplayHint
{
    private PlayerStatus playerStatus;
    public bool usable = true;
    public string displayTextOnHint = "Press E to eat ";
    public int useValue = -10;
	public bool firstEnter = false;

    void Start()
    {
        labelText = displayTextOnHint;
        playerStatus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
    }

    public override void Action()
    {
		if (SceneController.prevScene == "MainMenu") {
			if (HasCollided())
				firstEnter = true;
		}
		if (HasCollided() && Input.GetKeyDown("e")) {
			Close();
			playerStatus.addEnergyValue(useValue);
			// Destroy(this);
		}
    }
}