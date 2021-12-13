using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KampusScene : SceneController
{
    public Transform player;

    public override void Start()
    {
        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        if (_questTracker.getQuest(13).status == Quest.Status.completed || _questTracker.getQuest(13).status == Quest.Status.turnIn)
        {
            Destroy(GameObject.Find("Bone"));
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
    }
}