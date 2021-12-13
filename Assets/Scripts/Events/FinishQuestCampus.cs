using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishQuestCampus : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject gameController;
    PopUpMessage popupMessage;
    string requiredPrevScene = "ClassroomScene";
    private QuestTracker questTracker;


    void Start()
    {         
        //if (requiredPrevScene != SceneController.prevScene)
        //    return;

        ////if quest is completed already or quest isnt in inventory
        //var tmp = GameObject.FindGameObjectWithTag("StatusController");
        //if (tmp ?? false)
        //{
        //    questTracker = tmp.GetComponent<QuestTracker>();
        //}
        //else
        //    return;

        //Debug.Log("got quest tracker");
        //var questStatus = questTracker.ActiveQuest().GetStatus();
        //Debug.Log(questStatus);
        //if (questStatus != Quest.Status.progress)
        //    return;

        ////display that quest is done
        //popupMessage = gameController.GetComponent<PopUpMessage>();
        //StartCoroutine(DisplayText());

        ////set as completed
        //questTracker.ActiveQuest().ChangeStatus(Quest.Status.completed);
    }

    IEnumerator DisplayText()
    {
        yield return new WaitForSecondsRealtime(2);
        Sprite QuestIcon = Resources.LoadAll<Sprite>("PopUpMessageIcons")[0];
        popupMessage.Open(dialogue, optionalSprite: QuestIcon);
        GetComponent<ChangeScene>().Activate();
    }
}
