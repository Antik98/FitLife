using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTKScene : SceneController
{
    public Transform player;
    // Start is called before the first frame update
    public override void Start()
    {
        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        if (_questTracker.getQuest(14).status == Quest.Status.completed || _questTracker.getQuest(14).status == Quest.Status.turnIn)
        {
            Destroy(GameObject.Find("Pot"));
        }
        base.Start();
        if (prevScene == "KampusScene")
        {
            player.position = new Vector2(1.49f, -2.884f);
        }
        
    }

}
