using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class EasterEgg : DisplayHint
{
    public string displayTextOnHint = "Prozkoumej stisknutím E";
    public Dialogue dialogue;
    public int[] QuestIdFinishingHere;
    public int[] QuestIdTurningInHere;
    public int[] QuestIdStartingHere;
    public GameEffect gameEffect;
    private QuestTracker questTracker;
    public bool repeatableDialog;
    public bool shouldAddSocial = false;
    private bool dialogDone;
    public Sprite spriteOverrideDefault = null;
    public Sprite spriteOverride2 = null;
    public Sprite spriteOverride3 = null;
    PopUpMessage popupMessage;
 	GameObject gameController;
    PlayerStatus playerStatus;

    void Start() {
        dialogDone = false;
        labelText = displayTextOnHint;
        gameController = GameObject.Find("UI");
        popupMessage = gameController.GetComponent<PopUpMessage> ();
        questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        playerStatus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
    }

    public override void Action()
    {
        if (HasCollided() && Input.GetKeyDown("e"))
        {
            SpriteRenderer tmp = GetComponent<SpriteRenderer>();
            Sprite easterEgg = Resources.LoadAll<Sprite>("PopUpMessageIcons")[1];
            if(repeatableDialog || !dialogDone)
            {
                if (tmp != null && spriteOverrideDefault == null)
                    popupMessage.Open(dialogue, tmp.sprite);
                else if (spriteOverrideDefault != null || spriteOverride2 != null || spriteOverride3 != null)
                    popupMessage.Open(dialogue, spriteOverrideDefault, spriteOverride2, spriteOverride3);
                else
                    popupMessage.Open(dialogue, easterEgg);
                Close();
                dialogDone = true;
                StartCoroutine(WaitAfterDialog());
            }
        }
    }

    public IEnumerator WaitAfterDialog()
    {
        yield return new WaitWhile(popupMessage.isActive);
        if (shouldAddSocial)
            playerStatus.addSocialValue(1);
        executeAfterDialog();
        if (gameEffect != null)
        {
            StartCoroutine(gameEffect.execute());
            yield return new WaitWhile(gameEffect.isDone);
        }
    }


    public void executeAfterDialog()
    {
        foreach (var x in QuestIdFinishingHere)
        {
            if (questTracker.getQuest(x).GetStatus() == Quest.Status.turnIn)
            {
                Quest _questFinishingHere = questTracker.getQuest(x);
                questTracker.CompleteQuest(x);
                if (_questFinishingHere is QuestInteraction)
                {
                    string[] _tmp = { ((QuestInteraction)_questFinishingHere).questCompleteText };
                    popupMessage.Open(new Dialogue(_tmp), _questFinishingHere.getQuestIcon());
                }
            }
        }

        foreach (var x in QuestIdTurningInHere)
        {
            if (questTracker.getQuest(x).GetStatus() == Quest.Status.progress)
            {
                Quest _questFinishingHere = questTracker.getQuest(x);
                questTracker.TurnInQuest(x);
                if (_questFinishingHere is QuestInteraction)
                {
                    
                    string[] _tmp = { ((QuestInteraction)_questFinishingHere).questTurnInText };
                    popupMessage.Open(new Dialogue(_tmp), _questFinishingHere.getQuestIcon());
                }
            }
        }

        foreach (var x in QuestIdStartingHere)
        {
            if (questTracker.getQuest(x).GetStatus() == Quest.Status.inactive)
            {
                Quest _questFinishingHere = questTracker.getQuest(x);
                questTracker.AcceptQuest(x);
                if (_questFinishingHere is QuestInteraction)
                {
                    
                    string[] _tmp = { ((QuestInteraction)_questFinishingHere).questAcceptText };
                    popupMessage.Open(new Dialogue(_tmp), _questFinishingHere.getQuestIcon());
                }
            }
        }
    }
}