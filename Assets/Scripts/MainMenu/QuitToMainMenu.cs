using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToMainMenu : MonoBehaviour
{

    public void quitGame()
    {
        Debug.Log("Ending game");


        if (StatusController.Instance != null) StatusController.Instance.GetComponent<GameTimer>().StopTimer();
        if (MusicManager.Instance != null) MusicManager.Instance.GetComponent<ChangeMusicByScene>().restartMusic();
        if (StatusController.Instance != null) StatusController.Instance.Reset();
        Time.timeScale = 1;
        PauseGame.isGamePaused = false;
        SceneManager.LoadScene("MainMenu");
    }
}
