﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgtestQuest : Quest
{
    public TimeSpan deadline;

    public ProgtestQuest(int questID, string name, string text, TimeSpan deadline, Status status = Status.inactive) 
        : base(questID, name, text, status)
    {
        this.deadline = deadline;
        this.type = Quest.Type.progtest;
    }
    

    public override bool IsQuestTimedOut(TimeSpan gameTime)
    {
        return deadline < gameTime;
    }
}