using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pub2ndFloorSceneController : SceneController
{
    public Transform player;
    // Start is called before the first frame update
    public override void Start()
    {
        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        if (_questTracker.getQuest(15).status == Quest.Status.completed || _questTracker.getQuest(15).status == Quest.Status.turnIn)
        {
            Destroy(GameObject.Find("AAG"));
        }
        if (_questTracker.getQuest(13).status == Quest.Status.completed)
        {
            Destroy(GameObject.Find("Dog"));
        }
        base.Start();
        
    }

}
