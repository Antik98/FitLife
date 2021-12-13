using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    bool end = false;
    private PlayerStatus playerStatus;
    private WordManager wordManager;
    private GameTimer gameTimer;

    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
        gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        wordManager = GameObject.FindGameObjectWithTag("WordManager").GetComponent<WordManager>();
    }
    public void EndGame()
    {
        if( end == false )
        {
            end = true;
            Debug.Log("GAME OVER");
            SceneManager.LoadScene("CAO_MinigameLooseScreen");
        }
    }

    public void GameWin()
    {
        Debug.Log("GAME WON");
        wordManager.completed = true;
      //  gameTimer.StartTimer();
        SceneManager.LoadScene("CAO_MinigameWinningScreen");
    }
}
