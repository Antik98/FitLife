using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreenController : MonoBehaviour
{
    private GameTimer gameTimer;
    public ChangeScene changeScene;
    public bool success = true;
    public int questId;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => StatusController.initialized);
        Cursor.visible = false;
        gameTimer = StatusController.Instance.gameTimer;
        if(success)
            StatusController.Instance.coroutineQueue.list.Add((scene) => WaitForPlayerToComeBackSucc(scene, questId, "Quest splněn: zkouška dokončena!"));
        else
            StatusController.Instance.coroutineQueue.list.Add((scene) => WaitForPlayerToComeBackFail(scene, questId, "Quest nesplněn: zkouška nedokončena."));
    }
    void Update()
    {
        Cursor.visible = false;
        if (Input.GetKeyDown("e"))
        {
            gameTimer?.StartTimer();
            changeScene.Activate();
        }

    }

    bool WaitForPlayerToComeBackSucc(string scene, int questId, string dialogue = "")
    {
        if (scene == "KampusScene")
        {
            GameObject gameController = GameObject.Find("UI");
            PopUpMessage popupMessage = gameController.GetComponent<PopUpMessage>();
            Sprite QuestIcon = Resources.LoadAll<Sprite>("PopUpMessageIcons")[0];
            if (StatusController.Instance.questTracker.getQuest(questId) is SchoolQuest sq)
            {
                StatusController.Instance.questTracker.CompleteQuest(questId);
                gameTimer.SleepHours((float)(sq.deadline + TimeSpan.FromHours(1.5f) - gameTimer.gameTime).TotalHours);
            }
            popupMessage.Open(new Dialogue(dialogue), QuestIcon);
            StatusController.Instance.PlayerStatus.addStatValues(energyVal: -15, socialVal: -5, hungerVal: -10);
            return true;
        }
        return false;
    }

    bool WaitForPlayerToComeBackFail(string scene, int questId, string dialogue = "")
    {
        if (scene == "KampusScene")
        {
            GameObject gameController = GameObject.Find("UI");
            PopUpMessage popupMessage = gameController.GetComponent<PopUpMessage>();
            Sprite QuestIcon = Resources.LoadAll<Sprite>("PopUpMessageIcons")[0];
            if (StatusController.Instance.questTracker.getQuest(questId) is SchoolQuest sq)
            {
                StatusController.Instance.questTracker.FailQuest(questId);
                gameTimer.SleepHours((float)(sq.deadline + TimeSpan.FromHours(1.5f) - gameTimer.gameTime).TotalHours);
            }
            popupMessage.Open(new Dialogue(dialogue), QuestIcon);
            StatusController.Instance.PlayerStatus.addStatValues(energyVal: -15, socialVal: -5, hungerVal: -10);
            return true;
        }
        return false;
    }
}
