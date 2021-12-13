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
    private bool dialogDone;
    public Sprite spriteOverrideDefault = null;
    public Sprite spriteOverride2 = null;
    public Sprite spriteOverride3 = null;
    PopUpMessage popupMessage;
 	GameObject gameController;

    void Start() {
        dialogDone = false;
        labelText = displayTextOnHint;
        gameController = GameObject.Find("UI");
        popupMessage = gameController.GetComponent<PopUpMessage> ();
        questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
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
                    popupMessage.Open(dialogue, optionalSprite: tmp.sprite);
                else if (spriteOverrideDefault != null || spriteOverride2 != null || spriteOverride3 != null)
                    popupMessage.Open(dialogue, spriteOverrideDefault, spriteOverride2, spriteOverride3);
                else
                    popupMessage.Open(dialogue, optionalSprite: easterEgg);
                Close();
                dialogDone = true;
                StartCoroutine(WaitAfterDialog(executeAfterDialog));
            }
        }
    }

    public IEnumerator WaitAfterDialog(Action inpFunc)
    {
        yield return new WaitWhile(popupMessage.isActive);
        if(gameEffect != null)
        {
            StartCoroutine(gameEffect.execute());
            yield return new WaitWhile(gameEffect.isDone);
        }
        inpFunc();
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
                    StartCoroutine(WaitAfterDialog(() => popupMessage.Open(new Dialogue(_tmp), _questFinishingHere.getQuestIcon())));
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
                    StartCoroutine(WaitAfterDialog(() => popupMessage.Open(new Dialogue(_tmp), _questFinishingHere.getQuestIcon())));
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
                    StartCoroutine(WaitAfterDialog(() => popupMessage.Open(new Dialogue(_tmp), _questFinishingHere.getQuestIcon())));
                }
            }
        }
    }
}