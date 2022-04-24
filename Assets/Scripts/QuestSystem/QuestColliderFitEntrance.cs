﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestColliderFitEntrance : DisplayHint
{
    public string displayTextOnHint = "Vstoupit";
    private QuestTracker questTracker;
    private GameTimer gameTimer;
    private PopUpMessage popupMessage;
    private GameObject gameController;
    private ChangeScene sceneChanger;
    public GameObject questIcon;

    public Dialogue dialogue0;
    public Dialogue wellDoneDialogue;
    void Start()
    {
        labelText = displayTextOnHint;
        questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        gameController = GameObject.Find("UI");
        popupMessage = gameController.GetComponent<PopUpMessage>();
        sceneChanger = GetComponent<ChangeScene>();
    }

    private void openDialogue(Dialogue inp)
    {
        Sprite QuestIcon = Resources.LoadAll<Sprite>("PopUpMessageIcons")[0];
        popupMessage.Open(inp, QuestIcon);
    }

    public override void Action()
    {
        questIcon.SetActive(questTracker.getActiveQuests().Any(q => q is SchoolQuest && q.IsQuestFinishable(gameTimer.gameTime)));
        if (HasCollided() && Input.GetKeyDown("e"))
        {
            var activeQuests = questTracker.getActiveQuests();
            bool questUnavailable = true;
            foreach(Quest q in activeQuests)
            {
                if(q is SchoolQuest sq && q.IsQuestFinishable(gameTimer.gameTime))
                {
                    //Finish sluchatka quest
                    if (questTracker.getQuest(17).GetStatus() == Quest.Status.progress)
                    {
                        questTracker.TurnInQuest(17);
                        StatusController.Instance.GetComponent<CoroutineQueue>().list.Add((scene) => WaitForPlayerToComeBack(scene, (questTracker.getQuest(17) as QuestInteraction).questTurnInText));
                    }

                    questUnavailable = false;
                    questTracker.CompleteQuest(q.questID);
                    gameTimer.SleepHours((float)(sq.deadline + TimeSpan.FromHours(1.5f) - gameTimer.gameTime).TotalHours);
                    StatusController.Instance.GetComponent<CoroutineQueue>().list.Add((scene) => WaitForPlayerToComeBack(scene, wellDoneDialogue.sentences[0] + "Quest splněn:  " + q.name));
                    if (((SchoolQuest)q).schoolSceneName != "")
                    {
                        sceneChanger.Activate(((SchoolQuest)q).schoolSceneName);
                    }
                    break;
                }
            }
            if(questUnavailable)
                openDialogue(dialogue0);
        }
    }

    bool WaitForPlayerToComeBack(string scene, string dialogue = "")
    {
        if(scene == "KampusScene")
        {
            GameObject gameController = GameObject.Find("UI");
            PopUpMessage popupMessage = gameController.GetComponent<PopUpMessage>();
            Sprite QuestIcon = Resources.LoadAll<Sprite>("PopUpMessageIcons")[0];
            popupMessage.Open(new Dialogue(dialogue), QuestIcon);
            return true;
        }
        return false;
    }
}