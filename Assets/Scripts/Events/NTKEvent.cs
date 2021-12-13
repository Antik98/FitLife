using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NTKEvent : MonoBehaviour
{
    public GameObject UI;
    public string requiredPrevScene;
    public Dialogue dialogue;
    public bool isFinished;
    private bool dialogEnabled;
    private int keyCnt;
    public GameObject timer;
    void Start()
    {
        keyCnt = 0;
        isFinished = false;
        dialogEnabled = false;
        if (requiredPrevScene == SceneController.prevScene)
        {
            StartCoroutine(DisplayText());
        }
    }

    private void Update()
    {
        if (dialogEnabled && (keyCnt < 6) && Input.GetKeyDown(KeyCode.Space))
            keyCnt++;
        if (keyCnt == 6)
        {
            isFinished = true;
            keyCnt++;
        }
    }
    IEnumerator DisplayText()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        UI.GetComponent<PopUpMessage>().Open(dialogue);
        dialogEnabled = true;
        yield return new WaitForSeconds(1);
        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        _questTracker.AcceptQuest(16);
        _questTracker.TurnInQuest(16);
        Quest _ntkQuest = _questTracker.getQuest(16);
        string[] _tmp = { ((QuestInteraction)_ntkQuest).questTurnInText };
        UI.GetComponent<PopUpMessage>().Open(new Dialogue(_tmp), _ntkQuest.getQuestIcon());
        yield return new WaitForSeconds(1);
    }
}
