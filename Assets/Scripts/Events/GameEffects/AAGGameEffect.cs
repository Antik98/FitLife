using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAGGameEffect : GameEffect
{
    // Start is called before the first frame update
    public override IEnumerator execute()
    {
        yield return new WaitForSeconds(1);
        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        if (_questTracker.getQuest(15).status == Quest.Status.completed || _questTracker.getQuest(15).status == Quest.Status.turnIn)
        {
            Destroy(GameObject.Find("AAG"));
        }
        yield return new WaitForSeconds(0);
    }
}
