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
        if (StatusController.initialized)
            StatusController.Instance.Stop();
    }

    public void createNewGame()
    {
        if (StatusController.Instance != null)
            StatusController.Instance.Reset();
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        var fade = GameObject.Find("Fade");
        Animator transition = null;
        if (fade ?? false)
            transition = fade.GetComponent<Animator>();
        if (transition ?? false)
            transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        sceneController.LoadScene("HomeScene");
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
