using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationScript : MonoBehaviour
{
    public GameObject questMenu;
    //public PhoneDisplay phone;
    public GameObject pauseMenu;
    private PlayerStatus playerStatus;
    public PopUpMessage popUpMessage;

    private void Start()
    {
        questMenu.SetActive(false);
        playerStatus = StatusController.Instance.GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        if (!pauseMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Q) && !playerStatus.doTutorial && !popUpMessage.isActive())
            {
                if (questMenu.activeSelf)
                {
                    questMenu.SetActive(false);
                    GameObject.FindGameObjectWithTag("Player")?.GetComponent<playerMovement>().unlockPlayer();
                    StatusController.Instance.gameTimer.StartTimer();
                    Time.timeScale = 1f;
                }
                else
                {
                    questMenu.SetActive(true);
                    GameObject.FindGameObjectWithTag("Player")?.GetComponent<playerMovement>().lockPlayer();
                    StatusController.Instance.gameTimer.StopTimer();
                    Time.timeScale = 0f;
                }
            }
        }
    }

    /*
    public void OpenNotifications()
    {
        if(!questMenu.activeSelf)
        {
            questMenu.SetActive(true);
            if (phone.isActive())
            {
                phone.SetPhoneState(false);
            }
        }
    }

    public void CloseNotification()
    {
        questMenu.SetActive(false);

    }
 */
}
