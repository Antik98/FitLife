using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimerNtk : MonoBehaviour
{

    private float timer = 60;
    private bool canCount = true;
    private bool doOnce = false;
    public Text timerText;
    private QuestTracker questTracker;
    private PopUpMessage popUpMessage;
    private PlayerStatus playerStatus;
    private GameTimer gameTimer;

    // Start is called before the first frame update
    private void Start()
    {
        questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        popUpMessage = GameObject.FindGameObjectWithTag("UI").GetComponent<PopUpMessage>();
        playerStatus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
        gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
    }
    // Update is called once per frame 
    void Update()
    {
        if (timer >= 0f && canCount && !popUpMessage.isActive())
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString();
        }else if(timer <= 0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            timer = 0f;
            failedRun();
        }
    }

    void failedRun()
    {
        
        questTracker.FailQuest(16);
        QuestInteraction _ntkQuest = questTracker.getQuest(16) as QuestInteraction;
        popUpMessage.Open(new Dialogue(_ntkQuest.questTurnInText), _ntkQuest.getQuestIcon());

        playerStatus.addHungerValue(-20);
        playerStatus.addSocialValue(-20);
        playerStatus.addEnergyValue(-20);
        gameTimer.TriggerNextDay();
        this.GetComponent<ChangeScene>().Activate();
    }
}
