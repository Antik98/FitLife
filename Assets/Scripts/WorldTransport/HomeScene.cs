using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class HomeScene : SceneController {
 
    public Transform player;
 
    public override void Start () {
        base.Start();

        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        
        if (_questTracker.getQuest(13).status != Quest.Status.completed)
        {
            Destroy(GameObject.Find("Dog"));
        }
        base.Start();

        if (prevScene == "StrahovScene")
        {
            player.position = new Vector2(0.46f, -1.58f);
        }
    }
     
}