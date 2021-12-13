using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToMainMenu : MonoBehaviour
{

    public void quitGame()
    {
        Debug.Log("Ending game");


        if (StatusController.Instance != null) StatusController.Instance.GetComponent<GameTimer>().stoped = true;
        if (MusicManager.Instance != null) MusicManager.Instance.GetComponent<ChangeMusicByScene>().restartMusic();
        SceneManager.LoadScene("MainMenu");
    }
}
