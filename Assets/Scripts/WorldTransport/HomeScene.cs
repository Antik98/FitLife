using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScene : SceneController
{
    private QuestTracker _questTracker;
    public GameObject Dog;

    public override void Start()
    {
        base.Start();
        _questTracker = StatusController.Instance.GetComponent<QuestTracker>();
        if (_questTracker?.getQuest(13).status != Quest.Status.completed)
        {
            Dog.SetActive(false);
        }

        if (prevScene == "StrahovScene")
        {
            player.position = new Vector2(0.46f, -1.58f);
        }
    }

    public void Awake()
    {
        _questTracker = StatusController.Instance.GetComponent<QuestTracker>();
        if (_questTracker?.getQuest(13).status != Quest.Status.completed)
        {
            Dog.SetActive(false);
        }

    }
}