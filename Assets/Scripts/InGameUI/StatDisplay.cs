using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class StatDisplay : MonoBehaviour
{
    private PlayerStatus playerStatus;
    private GameTimer gameTimer;

    public PhoneDisplay phone;
    public Text time;   
    public Text hunger;
    public Text energy;
    public Text social;


    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());
        
    }
    private IEnumerator OnEnableCoroutine()
    {
        yield return new WaitUntil(() => StatusController.initialized);
        playerStatus = StatusController.Instance.GetComponent<PlayerStatus>();
        gameTimer = StatusController.Instance.GetComponent<GameTimer>();
        if (playerStatus != null)
        {
            playerStatus.HandleAttributesChanged += onAttributesChange;
            playerStatus.HandleEventTriggeredAttributeChanged += onAttributeChange;
        }
        if (gameTimer != null)
            gameTimer.BroadcastMinutePassed += onMinuteChange;
        onAttributesChange();
        onMinuteChange();

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

        List<Text> list = new List<Text>() { hunger, energy, social };

        foreach(Text s in list)
        {
            if (Int32.TryParse(s.text, out int output) && output <= 20)
            {
                if(phone != null)
                    phone.SetPhoneState(true);
                s.color = new Color32(181, 16, 16, 255);
                StatusController.Instance.audioManager.playSoundName("phoneVibration");
            }
            else
            {
                s.color = Color.black;
            }
            
        }
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
