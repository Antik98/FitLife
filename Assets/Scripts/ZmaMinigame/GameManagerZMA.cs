using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerZMA : MonoBehaviour
{
    private PlayerStatus playerStatus;
    private GameTimer gameTimer;
    private bool end = false;
    // Start is called before the first frame update
    private void Start()
    {
        gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        gameTimer.StopTimer();
        playerStatus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
    }

    public void GameWin()
    {
        Debug.Log("GAME WON");
       // gameTimer.StartTimer();
        SceneManager.LoadScene("ZMA_MinigameWinningScreen");
    }

    public void EndGame()
    {
        if (end == false)
        {
            Debug.Log("GAME OVER");
            end = true;
           // gameTimer.StartTimer();
            SceneManager.LoadScene("ZMA_MinigameLooseScreen");
        }
    }
}
