using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelQuestBtn : MonoBehaviour
{
    public QuestDisplay questDisplay;
    public QuestTracker questTracker;
    // Start is called before the first frame update
    void Start()
    {
        var tmp = GameObject.FindGameObjectWithTag("UI_Quests");
        if (tmp ?? false)
            questDisplay = tmp.GetComponent<QuestDisplay>();

        var tmp2 = GameObject.FindGameObjectWithTag("StatusController");
        if (tmp2 ?? false)
            questTracker = tmp2.GetComponent<QuestTracker>();



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if(questDisplay.displayedQuestIndex.HasValue)
            questTracker.FailQuest(questDisplay.displayedQuestIndex.Value);
    }


}
