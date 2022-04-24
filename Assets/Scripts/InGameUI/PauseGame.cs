using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject questMenu;
    public PopUpMessage popUpMessage;
    public GameObject cursor;

    public static bool isGamePaused { get; private set; }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => StatusController.initialized);
        if(Time.timeScale == 0)
        {
            OpenPauseMenu();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
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
                OpenPauseMenu();
            }
            
        }

    }

    private void OpenPauseMenu()
    {
        if (!popUpMessage.isActive())
        {
            GameObject.FindGameObjectWithTag("Player")?.GetComponent<playerMovement>().lockPlayer();
            StatusController.Instance.gameTimer.StopTimer();
        }
        cursor.SetActive(true);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    private void ClosePauseMenu()
    {
        if (!popUpMessage.isActive())
        {
            GameObject.FindGameObjectWithTag("Player")?.GetComponent<playerMovement>().unlockPlayer();
            StatusController.Instance.gameTimer.StartTimer();
        }
        cursor.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
}
