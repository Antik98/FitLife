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
    private TimeSpan deadline;
    public QuestInteraction(int questID,
        string name,
        string questLogText,
        string questAcceptText,
        string questTurnInText,
        string questCompleteText,
        Status status = Status.inactive,
        TimeSpan deadline = default(TimeSpan),
        string notysekText = null) : base( questID,  name,  questLogText, status, notysekText)
    {
        this.questID = questID;
        this.name = name;
        this.text = questLogText;
        this.status = status;
        this.questAcceptText = questAcceptText;
        this.questTurnInText = questTurnInText;
        this.questCompleteText = questCompleteText;
        this.deadline = deadline;
    }

    public override void CompleteQuest()
    {
        base.CompleteQuest();
        var playerStatus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
        playerStatus.addStatValues(socialVal: increaseSocial);
    }

    public override bool IsQuestTimedOut(TimeSpan gameTime)
    {
        if(deadline != default(TimeSpan))
            return gameTime > deadline;
        return base.IsQuestTimedOut(gameTime);
    }

}
