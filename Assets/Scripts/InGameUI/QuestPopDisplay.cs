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
        questTracker.HandleQuestChanged -= onQuestChange;
    }

    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());

    }

    private IEnumerator OnEnableCoroutine()
    {
        yield return new WaitUntil(() => StatusController.initialized);
        playerStatus = StatusController.Instance.GetComponent<PlayerStatus>();
        text.SetActive(false);
        questTracker = StatusController.Instance.GetComponent<QuestTracker>();
        questTracker.HandleQuestChanged += onQuestChange;
        UpdateQuestViewWithoutEffects();
    }

    void Update()
    {
        if (!pauseMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !playerStatus.doTutorial)
            {
                StopAllCoroutines();
                UpdateQuestViewWithoutEffects();
                questAnimator?.SetBool("IsOpen", !isActive());
                if (isActive())
                {
                    text.SetActive(true);
                }
                else
                {
                    text.SetActive(false);
                }
            }
        }
    }



    private void onQuestChange(object sender, int id)
    {
        StopAllCoroutines();
        StartCoroutine(UpdateQuestView());
        if(questTracker.getQuest(id).GetStatus() == Quest.Status.failed)
        {
            Sprite QuestIcon = Resources.LoadAll<Sprite>("PopUpMessageIcons")[0];
            popUpMessage.Open(new Dialogue("Úkol " + questTracker.getQuest(id).name + " nesplněn."), QuestIcon);
        }
    }

    private void UpdateQuestViewWithoutEffects()
    {
        questLines.ToList().Skip(1).ToList().ForEach(x => x.text = "");
        questLinesText.ToList().Skip(1).ToList().ForEach(x => x.text = "");
        var items = questLines.ToList().Zip(questLinesText.ToList().Zip(questTracker.getActiveQuests(), (x, y) => (x, y)), (x, y) => (x, y.x, y.y));
        foreach (var (title, text, quest) in items)
        {
            title.text = quest.name;
            text.text = quest.text;
        }

        if (!questTracker.getActiveQuests().Any())
        {
            questLines.FirstOrDefault().text = "";
            questLinesText.FirstOrDefault().text = "Máš volno! Žádné aktivní úkoly.";
        }
    }
        private IEnumerator UpdateQuestView()
    {
        bool wasOpen = isActive();
        questAnimator?.SetBool("IsOpen", true);

        questLines.ToList().Skip(1).ToList().ForEach(x => x.text = "");
        questLinesText.ToList().Skip(1).ToList().ForEach(x => x.text = "");
        var items = questLines.ToList().Zip(questLinesText.ToList().Zip(questTracker.getActiveQuests(), (x, y) => (x, y)), (x, y) => (x, y.x, y.y) );
        foreach (var (title, text, quest) in items)
        {
            StartCoroutine(UpdateText(title, quest.name));
            yield return StartCoroutine(UpdateText(text, quest.text));
        }

        if (!questTracker.getActiveQuests().Any())
        {
            questLines.FirstOrDefault().text = "";
            yield return StartCoroutine(DisplaySentence(questLinesText.FirstOrDefault(), "Máš volno! Žádné aktivní úkoly."));
        }

        questAnimator?.SetBool("IsOpen", wasOpen);
    }

    private IEnumerator UpdateText(TextMeshProUGUI gui, string newText)
    {
        WaitForSeconds w = new WaitForSeconds(0.02f);
        string originalStr = gui.text;
        if(newText != gui.text)
        {
            foreach (int x in Enumerable.Range(0, originalStr.Length))
            {
                gui.text = "<s>" + originalStr.Substring(0, x) + "</s>" + originalStr.Substring(x);
                yield return w;
            }
            yield return StartCoroutine(DisplaySentence(gui, newText));
        }
        yield return null;
    }

    IEnumerator DisplaySentence(TextMeshProUGUI gui, string display)
    {
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
