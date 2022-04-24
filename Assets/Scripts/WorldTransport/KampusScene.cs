﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KampusScene : SceneController
{
    public GameObject pingPong;
    public override void Start()
    {
        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        if (_questTracker.getQuest(13).status == Quest.Status.completed || _questTracker.getQuest(13).status == Quest.Status.turnIn)
        {
            Destroy(GameObject.Find("Bone"));
        }

        if (_questTracker.getQuest(18).status == Quest.Status.completed || _questTracker.getQuest(18).status == Quest.Status.turnIn)
        {
            Destroy(pingPong);
        }
        base.Start();

        if (prevScene == "StrahovScene")
        {
            player.position = new Vector2(5f, -1.3f);
        }
        else if (prevScene == "ClassroomScene")
        {
            player.position = new Vector2(-2.2f, 1f);
        }
        else if (prevScene == "NTK" || prevScene == "NTK_game")
        {
            player.position = new Vector2(3.126f, -1.41f);
        }
    }
}