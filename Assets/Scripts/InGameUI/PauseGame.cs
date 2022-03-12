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
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            phone.SetPhoneState(false);

            if (questMenu.activeSelf)
            {
                questMenu.SetActive(false);
            }
        }

    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
