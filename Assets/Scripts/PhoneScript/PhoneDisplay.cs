using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneDisplay : MonoBehaviour
{
    public GameObject ui;
    public GameObject popup;
    public GameObject pauseMenu;
    TutorialManager tutorialManager;

    // Update is called once per frame
    private void Start()
    {
        ui.SetActive(false);
        if(GameObject.FindGameObjectWithTag("TutorialTextBox"))
            tutorialManager = GameObject.FindGameObjectWithTag("TutorialTextBox").GetComponent<TutorialManager>();
    }
    public void OpenPhone()
    {
        ui.SetActive(true);
    }
    public void ClosePhone()
    {
        ui.SetActive(false);
    }
    public bool isActive()
    {
        return ui.activeSelf;
    }
    void Update()
    {
        if (!pauseMenu.activeSelf){
           
            if (popup.activeSelf)
            {
                ui.SetActive(false);
            }
            //switch visibility of 
            else if(Input.GetKeyDown(KeyCode.Tab) && tutorialManager != null && tutorialManager.canOpenPhone())
            {
                ui.SetActive(!ui.activeSelf);
            }
            else if(Input.GetKeyDown(KeyCode.Tab) && tutorialManager == null)
            {
                ui.SetActive(!ui.activeSelf);
            }
        }
    }

}
