using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgtestQuest : Quest
{
    public TimeSpan deadline;

    public ProgtestQuest(int questID, string name, string text, TimeSpan deadline, Status status = Status.inactive, string notysekText = null) 
        : base(questID, name, text, status, notysekText)
    {
        this.deadline = deadline;
        this.type = Quest.Type.progtest;
    }
    

    public override bool IsQuestTimedOut(TimeSpan gameTime)
    {
        return deadline < gameTime;
    }

    public override void CompleteQuest()
    {
        base.CompleteQuest();
        GradingSystem.GradeTracker.AddPoints(GradingSystem.SchoolSubjectType.PA1, 4);
    }
}