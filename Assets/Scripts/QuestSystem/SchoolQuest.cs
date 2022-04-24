using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using GradingSystem;
using UnityEngine;

[System.Serializable]
public class SchoolQuest : Quest
{
    public TimeSpan deadline { get; private set; }
    private SchoolSubjectType subject;
    private int gainedPointsToSubject;
    public string schoolSceneName;


    public SchoolQuest(int questID,
        string name,
        string text,
        TimeSpan deadline,
        Type type,
        SchoolSubjectType subject,
        int gainedPointsToSubject,
        string schoolSceneName,
        Status status = Status.inactive) 
        : base(questID, name, text, status)
    {
        this.deadline = deadline;
        this.type = type;
        this.subject = subject;
        this.gainedPointsToSubject = gainedPointsToSubject;
        this.schoolSceneName = schoolSceneName;
    }


    public override void CompleteQuest()
    {
        base.CompleteQuest();
        GradingSystem.GradeTracker.AddPoints(subject, gainedPointsToSubject);
        var playerStatus = StatusController.Instance.GetComponent<PlayerStatus>();
    }

    public override bool IsQuestTimedOut(TimeSpan gameTime)
    {
        return gameTime > deadline + TimeSpan.FromMinutes(15f); // is it past deadline?
    }

    public override bool IsQuestFinishable(TimeSpan gameTime)
    {
        return gameTime >= deadline - TimeSpan.FromMinutes(15f) && gameTime <= deadline + TimeSpan.FromMinutes(15f);
    }
}