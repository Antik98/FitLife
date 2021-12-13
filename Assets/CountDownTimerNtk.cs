using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimerNtk : MonoBehaviour
{

    private float timer;
    private bool canCount = true;
    private bool doOnce = false;
    private bool finished = false;

    // Start is called before the first frame update
    // Update is called once per frame 
    void Update()
    {
        if (timer >= 0f && canCount)
        {
            timer -= Time.deltaTime;
        }else if(timer <= 0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            timer = 0f;
            finished = true;
            StartCoroutine(failedRun());
        }
    }

    IEnumerator failedRun()
    {
        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        _questTracker.FailQuest(16);
        Quest _ntkQuest = _questTracker.getQuest(16);
        string[] _tmp = { ((QuestInteraction)_ntkQuest).questAcceptText };
        GameObject.FindGameObjectWithTag("UI").GetComponent<PopUpMessage>().Open(new Dialogue(_tmp), _ntkQuest.getQuestIcon());
        yield return new WaitForSeconds(1);
        PlayerStatus _playerStatus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
        _playerStatus.addHungerValue(-20);
        _playerStatus.addSocialValue(-20);
        _playerStatus.addEnergyValue(-20);
        GameTimer _gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        _gameTimer.TriggerNextDay();
        this.GetComponent<ChangeScene>().Activate();
    }
    public bool isTimerFinished()
    {
        return finished;
    }
}
