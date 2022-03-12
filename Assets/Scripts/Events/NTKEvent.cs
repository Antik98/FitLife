using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NTKEvent : MonoBehaviour
{
    public GameObject UI;
    public string requiredPrevScene;
    public Dialogue dialogue;
    void Start()
    {
        DisplayText();
    }

    void DisplayText()
    {
        UI.GetComponent<PopUpMessage>().Open(dialogue);
        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        _questTracker.AcceptQuest(16);
        _questTracker.TurnInQuest(16);
        QuestInteraction _ntkQuest = _questTracker.getQuest(16) as QuestInteraction;
        UI.GetComponent<PopUpMessage>().Open(new Dialogue(_ntkQuest.questTurnInText), _ntkQuest.getQuestIcon());
    }
}
