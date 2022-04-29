using System;
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
                    //Finish isic quest
                    if (questTracker.getQuest(17).GetStatus() == Quest.Status.progress)
                    {
                        questTracker.TurnInQuest(17);
                        StatusController.Instance.GetComponent<CoroutineQueue>().list.Add((scene) => WaitForPlayerToComeBack(scene, 17, (questTracker.getQuest(17) as QuestInteraction).questTurnInText));
                    }
                    questUnavailable = false;
                    if(sq.questID == 6 || sq.questID == 7)
                    {
                        sceneChanger.Activate(((SchoolQuest)q).schoolSceneName);
                        break;
                    }

                    StatusController.Instance.GetComponent<CoroutineQueue>().list.Add((scene) => WaitForPlayerToComeBack(scene, q.questID, wellDoneDialogue.sentences[0] + "Quest splněn: " + q.name));
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

    bool WaitForPlayerToComeBack(string scene, int questId, string dialogue = "")
    {
        if(scene == "KampusScene")
        {
            GameObject gameController = GameObject.Find("UI");
            PopUpMessage popupMessage = gameController.GetComponent<PopUpMessage>();
            Sprite QuestIcon = Resources.LoadAll<Sprite>("PopUpMessageIcons")[0];
            if (StatusController.Instance.questTracker.getQuest(questId) is SchoolQuest sq)
            {
                questTracker.CompleteQuest(questId);
                gameTimer.SleepHours((float)(sq.deadline + TimeSpan.FromHours(1.5f) - gameTimer.gameTime).TotalHours);
            }
            popupMessage.Open(new Dialogue(dialogue), QuestIcon);
            StatusController.Instance.PlayerStatus.addStatValues(energyVal: -15, socialVal: -5, hungerVal: -10);
            return true;
        }
        return false;
    }
}