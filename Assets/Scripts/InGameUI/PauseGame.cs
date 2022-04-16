using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    public PhoneDisplay phone;
    public GameObject questMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (questMenu.activeSelf)
            {
                questMenu.SetActive(false);
            }

            if (pauseMenu.activeSelf)
            {
                ClosePauseMenu();
            }
            else
            {
                GameObject.FindGameObjectWithTag("Player")?.GetComponent<playerMovement>().lockPlayer();
                StatusController.Instance.gameTimer.StopTimer();
                pauseMenu.SetActive(true);
                phone.SetPhoneState(false);
            }
            
        }

    }

    public void ClosePauseMenu()
    {
        GameObject.FindGameObjectWithTag("Player")?.GetComponent<playerMovement>().unlockPlayer();
        pauseMenu.SetActive(false);
        StatusController.Instance.gameTimer.StartTimer();
    }
}
