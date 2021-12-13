using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PubScene : SceneController
{
    public Transform player;
    

    public override void Start()
    {
        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        if (_questTracker.getQuest(10).status == Quest.Status.completed)
        {
            Destroy(GameObject.Find("Filip"));
            Destroy(GameObject.Find("Kosta"));
            Destroy(GameObject.Find("Kosta a Filip"));
        }
        base.Start();
        if (prevScene == "Pub2ndFloorScene")
        {
            player.position = new Vector2(2.566f, 1.517f);
        }
        if (prevScene == "StrahovScene")
        {
            player.position = new Vector2(1.458f, -1.611f);
        }
        
    }
}
