using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public abstract class Quest
{
    public enum Status
    {
        inactive,
        progress,
        turnIn,
        completed,
        failed,
        canceled
    }
    public enum Type
    {
        exam,
        proseminar,
        lecture,
        practice,
        progtest
    }
    public int questID = 0;
    public string name = "";
    public string text = "";
    public Type type;
    public Status status;


    public Quest(int questID, string questName, string questLogText, Status status = Status.inactive)
    {
        this.questID = questID;
        this.name = questName;
        this.text = questLogText;
        this.status = status;
    }

    public virtual Status GetStatus()
    {
        return status;
    }

    public virtual void ChangeStatus(Status status)
    {
        this.status = status;
    }

    public virtual void ChangeQuestText(string text)
    {
        this.text = text;
    }

    public virtual void CompleteQuest()
    {
        status = Status.completed;
    }

    public virtual void AcceptQuest()
    {
        status = Status.progress;
    }

    public virtual void TurnInQuest()
    {
        status = Status.turnIn;
    }
    public virtual void CancelQuest()
    {
        status = Status.canceled;
    }

    public virtual void FailQuest()
    {
        status = Status.failed;
    }

    public Sprite getQuestIcon()
    {
        return Resources.LoadAll<Sprite>("PopUpMessageIcons")[0];
    }

    public virtual bool IsQuestTimedOut(TimeSpan gameTime)
    {
        return false;
    }

    public virtual bool IsQuestFinishable(TimeSpan gameTime)
    {
        return true;
    }
}
