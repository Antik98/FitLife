using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestDisplay : MonoBehaviour
{
    private QuestTracker questTracker;
    public Dictionary<int,GameObject> questObjects = new Dictionary<int, GameObject>();
    public GameObject content;
    public GameObject template;
    //public Text questText;
    public TextMeshProUGUI questText;
    public int? displayedQuestIndex = 0;


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
        questTracker = StatusController.Instance.GetComponent<QuestTracker>();
        questTracker.HandleQuestChanged += onQuestChange;
        GetQuests();
    }

    private void onQuestChange(object sender, int id)
    {
        UpdateQuestView(id);
    }

    public void GetQuests()
    {
        List<Quest> activeQuests = StatusController.Instance.GetComponent<QuestTracker>().getQuests();
        foreach(Quest q in activeQuests)
        {
            UpdateQuestView(q.questID);
        }
    }


    public void UpdateQuestView( int num )
    {
        Quest.Status curStatus = questTracker.getQuest(num).GetStatus();

        if (!QuestExists(num))
        {
            if (curStatus == Quest.Status.progress || curStatus == Quest.Status.turnIn)
            {
                var copy = Instantiate(template);
                copy.GetComponent<TextMeshProUGUI>().text = questTracker.getQuest(num).name;
                copy.transform.SetParent( content.GetComponent<GridLayoutGroup>().transform, false);
                copy.GetComponent<QuestFab>().questNumber = num;
                questObjects.Add(num, copy.gameObject);
            }

        }
        else // remove the quest if the status is different
        {
            if (curStatus != Quest.Status.progress && curStatus != Quest.Status.turnIn)
            {
                if (questObjects.ContainsKey(num))
                {
                    Destroy(questObjects[num]);
                    questObjects.Remove(num);
                    displayedQuestIndex = null;
                }
            }
        }
        UpdateViewedQuestText();
    }

    //takes OnClick from view 
    public void UpdateViewedQuestText(int? num = null)
    {
        if (!num.HasValue && questObjects.Count() > 0)
        {
            num = questObjects.First().Value.GetComponent<QuestFab>().questNumber;
        }

        questText.text = num.HasValue ? questTracker.getQuest(num.Value).text : "Žádný aktivní quest, nezapomeň se zítra ráno podívat na nové questy dle rozvrhu.";
        displayedQuestIndex = num ?? null;
    }

    public bool QuestExists(int num)
    {
        if(questObjects != null && questObjects.ContainsKey(num) )
            return true;
        return false;
    }

    public void RemoveQuest(int num)
    {
        if(QuestExists(num))
            questTracker.FailQuest(num);
    }
    public bool isActive()
    {
        return gameObject.activeSelf;
    }
}
