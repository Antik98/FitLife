using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollider : DisplayHint
{
    private QuestTracker questTracker;
    public int questNumber = 0;

    void Start()
    {
        questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
    }


    public override void Action()
    {
        if (!HasCollided())
            return;

        //if( questTracker.activeQuestIndex == 0 )
        //{


        //    if (questTracker.ActiveQuest().GetStatus() == Quest.Status.inactive)
        //    {
        //        labelText = "Press E to acceept quest";
        //    }
        //    else if (questTracker.ActiveQuest().GetStatus() == Quest.Status.progress)
        //    {
        //        labelText = "Press E to complete quest requirement";
        //    }

        //    else if (questTracker.ActiveQuest().GetStatus() == Quest.Status.inactive)
        //    {
        //        labelText = "Press E to complete quest";
        //    }

        //    if (Input.GetKeyDown("e"))
        //    {
        //        int tmp = questTracker.activeQuestIndex;

        //        if (questTracker.ActiveQuest().GetStatus() == Quest.Status.turnIn)
        //        {
        //            Debug.Log("turnIn - " + tmp.ToString());
        //            questTracker.CompleteQuest(0);
        //        }

        //        else
        //        {
        //            Debug.Log("WrongProcess - " + tmp.ToString());
        //        }
        //    }
        //}

    }


}
