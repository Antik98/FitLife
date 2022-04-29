using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToMainMenu : MonoBehaviour
{

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space) && SceneManager.GetActiveScene().name == "endingScene")
        {
            Debug.Log("Ending game");


            if (StatusController.Instance != null) StatusController.Instance.GetComponent<GameTimer>().StopTimer();
            if (StatusController.Instance != null) StatusController.Instance.audioManager.restartMusic();
            if (StatusController.Instance != null) StatusController.Instance.Reset();
            Time.timeScale = 1;
            PauseGame.isGamePaused = false;
            SceneManager.LoadScene("MainMenu");
        }
    }


    public void QuitGame()
    {
            Debug.Log("Ending game");


            if (StatusController.Instance != null) StatusController.Instance.GetComponent<GameTimer>().StopTimer();
            if (StatusController.Instance != null) StatusController.Instance.audioManager.restartMusic();
            if (StatusController.Instance != null) StatusController.Instance.Reset();
            Time.timeScale = 1;
            PauseGame.isGamePaused = false;
            SceneManager.LoadScene("MainMenu");
    }
}



