




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class EasterEgg : DisplayHint
{
    public string displayTextOnHint = "Prozkoumat";
    public Dialogue dialogue;
    public Dialogue questFinishDialogue;
    public int[] QuestIdFinishingHere;
    public int[] QuestIdTurningInHere;
    public int[] QuestIdStartingHere;
    public GameEffect gameEffect;
    private QuestTracker questTracker;
    public bool repeatableDialog;
    public bool isEasterEgg = false;
    public bool shouldAddSocial = false;
    private bool dialogDone => !repeatableDialog && !StatusController.Instance.interactionTracker.isInteractionAvailable(dialogue.GetHashCode());
    public bool questInteractionAvailable => QuestIdStartingHere.Any(s=>questTracker.getQuest(s).GetStatus() == Quest.Status.inactive)
        || QuestIdTurningInHere.Any(s => questTracker.getQuest(s).GetStatus() == Quest.Status.progress)
        || QuestIdFinishingHere.Any(s => questTracker.getQuest(s).GetStatus() == Quest.Status.turnIn);
    public Sprite spriteOverrideDefault = null;
    public Sprite spriteOverride2 = null;
    public Sprite spriteOverride3 = null;
    public GameObject questIndicator;
    PopUpMessage popupMessage;
 	GameObject gameController;
    PlayerStatus playerStatus;

    void Start() {
        labelText = displayTextOnHint;
        gameController = GameObject.Find("UI");
        popupMessage = gameController.GetComponent<PopUpMessage> ();
        questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        playerStatus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
    }

    public override void Action()
    {
        displayHint = !dialogDone || questInteractionAvailable;
        if(questIndicator != null)
            questIndicator?.SetActive(questInteractionAvailable);
        if (HasCollided() && Input.GetKeyDown("e") && StatusController.Instance.interactionTracker.isInteractionSelected(base.GetHashCode()))
        {
            if(!dialogDone && !popupMessage.isActive())
            {
                displayHint = false;
                DisplayDialogueWithOverrideSprites(dialogue);
                StartCoroutine(giveSocial());
                Close();
                StatusController.Instance.interactionTracker.addInteractionToHistory(dialogue.GetHashCode(), isEasterEgg);
            }
            StartCoroutine(questInteractionProcess());
        }
    }

    private void DisplayDialogueWithOverrideSprites(Dialogue dia)
    {
        SpriteRenderer tmp = GetComponent<SpriteRenderer>();
        Sprite easterEgg = Resources.LoadAll<Sprite>("PopUpMessageIcons")[1];
        if (tmp != null && spriteOverrideDefault == null)
            popupMessage.Open(dia, tmp.sprite);
        else if (spriteOverrideDefault != null || spriteOverride2 != null || spriteOverride3 != null)
            popupMessage.Open(dia, spriteOverrideDefault, spriteOverride2, spriteOverride3);
        else
            popupMessage.Open(dia, easterEgg);
    }

    public IEnumerator giveSocial()
    {
        yield return new WaitWhile(popupMessage.isActive);
        if (shouldAddSocial)
            playerStatus.addStatValues(socialVal: 1);
    }

    public IEnumerator WaitAfterDialog()
    {
        yield return new WaitWhile(popupMessage.isActive);
        if (gameEffect != null)
        {
            StartCoroutine(gameEffect.execute());
            yield return new WaitUntil(() => gameEffect.done);
        }
    }


    public IEnumerator questInteractionProcess()
    {
        yield return new WaitWhile(() => popupMessage?.isActive() ?? false);

        foreach (var x in QuestIdFinishingHere)
        {
            if (questTracker.getQuest(x).GetStatus() == Quest.Status.turnIn)
            {
                Quest _questFinishingHere = questTracker.getQuest(x);
                if (shouldAddSocial)
                    playerStatus.addStatValues(socialVal: 10);
                questTracker.CompleteQuest(x);
                DisplayDialogueWithOverrideSprites(questFinishDialogue);
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
        yield return new WaitWhile(() => popupMessage?.isActive() ?? false);
        StartCoroutine(WaitAfterDialog());
    }
}