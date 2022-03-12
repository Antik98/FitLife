using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatDisplay : MonoBehaviour
{
    private PlayerStatus playerStatus;
    private GameTimer gameTimer;

    public Text time;   
    public Text hunger;
    public Text energy;
    public Text social;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        playerStatus = StatusController.Instance.GetComponent<PlayerStatus>();
        gameTimer = StatusController.Instance.GetComponent<GameTimer>();

        playerStatus.HandleAttributesChanged += onAttributesChange;
        playerStatus.HandleEventTriggeredAttributeChanged += onAttributeChange;
        gameTimer.BroadcastMinutePassed += onMinuteChange;

        onAttributesChange();
    }

    private void OnEnable()
    {
        if(playerStatus != null)
        {
            playerStatus.HandleAttributesChanged += onAttributesChange;
            playerStatus.HandleEventTriggeredAttributeChanged += onAttributeChange;
        }
        if(gameTimer != null)
            gameTimer.BroadcastMinutePassed += onMinuteChange;
    }

    private void OnDisable()
    {
        if(playerStatus != null)
        {
            playerStatus.HandleAttributesChanged -= onAttributesChange;
            playerStatus.HandleEventTriggeredAttributeChanged -= onAttributeChange;
        }

        if (gameTimer != null)
            gameTimer.BroadcastMinutePassed -= onMinuteChange;
    }

    private void onAttributesChange()
    {
        hunger.text = playerStatus.hunger.ToString();
        energy.text = playerStatus.energy.ToString();
        social.text = playerStatus.social.ToString();
    }

    private void onAttributeChange(object sender, PlayerStatus.Stats stats, int value)
    {
        onAttributesChange();
    }

    private void onMinuteChange()
    {
        time.text = gameTimer.GetCurrentTimeString();
    }

}
