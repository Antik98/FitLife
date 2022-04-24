using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearGameEffect : GameEffect
{
    public override IEnumerator execute()
    {
        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        if (_questTracker.getQuest(18).status == Quest.Status.completed || _questTracker.getQuest(18).status == Quest.Status.turnIn)
        {
            Destroy(gameObject);
        }
        done = true;
        yield return new WaitForSeconds(0);
    }
}
