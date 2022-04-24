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
        UpdateQuestView();
    }

    void Update()
    {
        if (!pauseMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !playerStatus.doTutorial)
            {
                UpdateQuestView();
                questAnimator.SetBool("IsOpen", !isActive());
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
        UpdateQuestView();
        if(questTracker.getQuest(id).GetStatus() == Quest.Status.failed)
        {
            Sprite QuestIcon = Resources.LoadAll<Sprite>("PopUpMessageIcons")[0];
            popUpMessage.Open(new Dialogue("Úkol " + questTracker.getQuest(id).name + " nesplněn."), QuestIcon);
        }
    }


    public void UpdateQuestView()
    {
        questLines.ToList().ForEach(x => x.text = "");
        questLinesText.ToList().ForEach(x => x.text = "");
        questLinesText.FirstOrDefault().text = "Máš volno! Žádné aktivní úkoly.";
        var items = questLines.ToList().Zip(questLinesText.ToList().Zip(questTracker.getActiveQuests(), (x, y) => (x, y)), (x, y) => (x, y.x, y.y) );
        foreach (var (title, text, quest) in items)
        {
            title.text = quest.name;
            text.text = quest.text;
        }
    }

    public bool isActive()
    {
        return questAnimator.GetBool("IsOpen");
    }
}
