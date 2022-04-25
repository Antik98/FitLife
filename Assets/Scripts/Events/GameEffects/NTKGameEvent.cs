using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTKGameEvent : GameEffect
{
    public override IEnumerator execute()
    {
        yield return null;
        StatusController.Instance.questTracker.CompleteQuest(16);
        StatusController.Instance.questTracker.CompleteQuest(9);
        StatusController.Instance.coroutineQueue.list.Add((scene) => WaitForPlayerToComeBack(scene));
        done = true;
        this.GetComponent<ChangeScene>().Activate();
    }

    bool WaitForPlayerToComeBack(string scene)
    {
        if (scene == "KampusScene")
        {
            GameObject gameController = GameObject.Find("UI");
            PopUpMessage popupMessage = gameController.GetComponent<PopUpMessage>();
            Sprite QuestIcon = Resources.LoadAll<Sprite>("PopUpMessageIcons")[0];
            popupMessage.Open(new Dialogue("Výborně! Progtest hotov a dokonce na plný počet i s bonusem."), QuestIcon);
            StatusController.Instance.PlayerStatus.addStatValues(socialVal: -10);
            return true;
        }
        return false;
    }
}
