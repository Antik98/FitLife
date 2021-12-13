using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestInteraction : Quest
{
    public string questAcceptText;
    public string questTurnInText;
    public string questCompleteText;
    private static readonly int increaseSocial = 5;  
    public QuestInteraction(int questID,
        string name,
        string questLogText,
        string questAcceptText,
        string questTurnInText,
        string questCompleteText,
        Status status = Status.inactive) : base( questID,  name,  questLogText, status)
    {
        this.questID = questID;
        this.name = name;
        this.text = questLogText;
        this.status = status;
        this.questAcceptText = questAcceptText;
        this.questTurnInText = questTurnInText;
        this.questCompleteText = questCompleteText;
    }

    public override void CompleteQuest()
    {
        base.CompleteQuest();
        var playerStatus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
        playerStatus.social += increaseSocial; 
    }

}
