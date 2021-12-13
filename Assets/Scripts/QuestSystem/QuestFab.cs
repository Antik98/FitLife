using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFab : MonoBehaviour
{
    public QuestDisplay questDisplay;
    public int questNumber = 0;

    void Start()
    {
        var tmp = GameObject.FindGameObjectWithTag("UI_Quests");
        if (tmp ?? false)
            questDisplay = tmp.GetComponent<QuestDisplay>();
    }

    public QuestFab( int questNumber )
    {
        this.questNumber = questNumber;
    }

    public void OnClick ()
    {
        questDisplay.displayedQuestIndex = questNumber;
        questDisplay.UpdateViewedQuestText(questNumber);
    }

}
