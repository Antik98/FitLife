using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class QuestPopDisplay : MonoBehaviour
{
    public GameObject pauseMenu;
    public Animator questAnimator;
    private PlayerStatus playerStatus;
    public GameObject text;
    public PopUpMessage popUpMessage;


    private QuestTracker questTracker;
    public TextMeshProUGUI[] questLines;
    public TextMeshProUGUI[] questLinesText;

    private void OnDisable()
    {
        if(questTracker != null)
            questTracker.HandleQuestChanged -= onQuestChange;
    }

    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());

    }

    private IEnumerator OnEnableCoroutine()
    {
        yield return new WaitUntil(() => StatusController.initialized);
        playerStatus = StatusController.Instance.PlayerStatus;
        text.SetActive(false);
        questTracker = StatusController.Instance.questTracker;
        questTracker.HandleQuestChanged += onQuestChange;
        if (playerStatus.doTutorial)
            StartCoroutine(forceUpdateView());
        else
            UpdateQuestViewWithoutEffects();
    }

    void Update()
    {
        if (!pauseMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !playerStatus.doTutorial)
            {
                UpdateQuestViewWithoutEffects();
                questAnimator?.SetBool("IsOpen", !isActive());
                text.SetActive(isActive());
            }
        }
    }

    private IEnumerator forceUpdateView()
    {
        questAnimator?.SetBool("IsOpen", true);
        questLines.ToList().ForEach(x => x.text = "");
        questLinesText.ToList().ForEach(x => x.text = "");
        yield return StartCoroutine(UpdateQuestView());
        questAnimator?.SetBool("IsOpen", false);
    }

    private void onQuestChange(object sender, int id)
    {
        if(sender == null)
        {
            StartCoroutine(forceUpdateView());
            return;
        }

        if (questTracker.getQuest(id).GetStatus() == Quest.Status.turnIn)
        {
            return;
        }

        StopAllCoroutines();
        StartCoroutine(UpdateQuestView(id));
        if(questTracker.getQuest(id).GetStatus() == Quest.Status.failed)
        {
            Sprite QuestIcon = Resources.LoadAll<Sprite>("PopUpMessageIcons")[0];
            popUpMessage.Open(new Dialogue("Úkol " + questTracker.getQuest(id).name + " nesplněn."), QuestIcon);
        }
    }

    private void UpdateQuestViewWithoutEffects()
    {
        StopAllCoroutines();
        questLines.ToList().ForEach(x => x.text = "");
        questLinesText.ToList().ForEach(x => x.text = "");
        var items = questLines.ToList().Zip(questLinesText.ToList().Zip(questTracker.getActiveQuests(), (x, y) => (x, y)), (x, y) => (x, y.x, y.y));
        foreach (var (title, text, quest) in items)
        {
            title.text = quest.name;
            text.text = quest.notysekText;
        }

        if (!questTracker.getActiveQuests().Any())
        {
            questLines.FirstOrDefault().text = "";
            questLinesText.FirstOrDefault().text = "Žádné aktivní úkoly, pro dnešek mám volno! Zítra ráno se nesmím zapomenout podívat na nové questy dle rozvrhu.";
        }
        else if (items.Count() == 1)
        {
            questLines.ToList().Skip(1).ToList().ForEach(x => x.text = "");
            questLinesText.ToList().Skip(1).ToList().ForEach(x => x.text = "");
        }
    }

    private IEnumerator UpdateQuestView(int? questId = null)
    {
        bool wasOpen = isActive();
        var items = questLines.ToList().Zip(
            questLinesText.ToList().Zip(
            questTracker.getActiveQuests(), (x, y) => (x, y)), (x, y) => (x, y.x, y.y) );

        foreach (var (title, text, quest) in items)
        {
            if(questId.HasValue && questTracker.getQuest(questId.Value).name == title.text)
            {
                StartCoroutine(ScratchText(title, quest.name));
                yield return StartCoroutine(ScratchText(text, quest.notysekText));

            }
            StartCoroutine(DisplaySentence(title, quest.name));
            yield return StartCoroutine(DisplaySentence(text, quest.notysekText));
        }

        if (!questTracker.getActiveQuests().Any())
        {
            questLines.FirstOrDefault().text = "";
            yield return StartCoroutine(DisplaySentence(questLinesText.FirstOrDefault(), "Máš volno! Žádné aktivní úkoly."));
        } else if (items.Count() == 1)
        {
            if (questId.HasValue && questTracker.getQuest(questId.Value).name == questLines.ElementAtOrDefault(1)?.text)
            {
                StartCoroutine(ScratchText(questLines[1], ""));
                yield return StartCoroutine(ScratchText(questLinesText[1], ""));
            }
            else
            {
                questLines.ToList().Skip(1).ToList().ForEach(x => x.text = "");
                questLinesText.ToList().Skip(1).ToList().ForEach(x => x.text = "");
            }
        }
        questAnimator?.SetBool("IsOpen", wasOpen);
    }

    private IEnumerator ScratchText(TextMeshProUGUI gui, string newText)
    {
        WaitForSeconds w = new WaitForSeconds(0.02f);
        string originalStr = gui.text;
        if(newText != gui.text)
        {
            questAnimator?.SetBool("IsOpen", true);
            foreach (int x in Enumerable.Range(0, originalStr.Length))
            {
                gui.text = "<s>" + originalStr.Substring(0, x) + "</s>" + originalStr.Substring(x);
                yield return w;
            }
        }
        yield return null;
    }

    IEnumerator DisplaySentence(TextMeshProUGUI gui, string display)
    {
        if (display == gui.text)
            yield break;
        questAnimator?.SetBool("IsOpen", true);
        StatusController.Instance.audioManager.playSoundName("writing");
        gui.text = "";
        WaitForSeconds w = new WaitForSeconds(0.02f);
        foreach (char letter in display.ToCharArray())
        {
            gui.text += letter;
            yield return w;
        }
    }

    public bool isActive()
    {
        return questAnimator.GetBool("IsOpen");
    }
}
