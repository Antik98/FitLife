using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneQuestGameEffect : GameEffect
{
    public override IEnumerator execute()
    {
        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        if (_questTracker.getQuest(13).status == Quest.Status.completed || _questTracker.getQuest(13).status == Quest.Status.turnIn)
        {
            Destroy(GameObject.Find("Bone"));
        }
        yield return new WaitForSeconds(0);
    }
}
