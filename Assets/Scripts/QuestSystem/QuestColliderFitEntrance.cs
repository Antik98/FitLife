using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestColliderFitEntrance : DisplayHint
{
    public string displayTextOnHint = "Prozkoumej stisknutím E";
    private QuestTracker questTracker;
    private GameTimer gameTimer;
    private PopUpMessage popupMessage;
    private GameObject gameController;
    private ChangeScene sceneChanger;

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
        popupMessage.Open(inp, optionalSprite: QuestIcon);
    }

    public override void Action()
    {
        if (HasCollided() && Input.GetKeyDown("e"))
        {
            var activeQuests = questTracker.getActiveQuests();
            bool questUnavailable = true;
            foreach(Quest q in activeQuests)
            {
                if(q is SchoolQuest && q.IsQuestFinishable(gameTimer.gameTime))
                {
                    questUnavailable = false;
                    questTracker.CompleteQuest(q.questID);
                    StatusController.Instance.GetComponent<CoroutineQueue>().list.Add((scene) => WaitForPlayerToComeBack(scene, " " + q.name + " hotovo!"));
                    if (((SchoolQuest)q).schoolSceneName != "")
                    {
                        sceneChanger.Activate(((SchoolQuest)q).schoolSceneName);
                    }
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
            wellDoneDialogue.sentences[0] += dialogue;
            popupMessage.Open(wellDoneDialogue, optionalSprite: QuestIcon);
            return true;
        }
        return false;
    }
}