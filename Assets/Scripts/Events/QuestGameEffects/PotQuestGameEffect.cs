using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotQuestGameEffect : GameEffect
{
    public override IEnumerator execute()
    {
        yield return new WaitForSeconds(1);
        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        if (_questTracker.getQuest(14).status == Quest.Status.completed || _questTracker.getQuest(14).status == Quest.Status.turnIn)
        {
            Destroy(GameObject.Find("Pot"));
        }
        yield return new WaitForSeconds(0);
    }
}
