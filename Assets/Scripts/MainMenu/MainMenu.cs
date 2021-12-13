using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private SceneController sceneController;

    void Start()
    {
        
        sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneController>();
    }

    public void createNewGame()
    {
        if (StatusController.Instance != null)
            StatusController.Instance.Reset();
        sceneController.LoadScene("HomeScene");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
