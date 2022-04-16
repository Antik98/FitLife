using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreenController : MonoBehaviour
{
    private GameTimer gameTimer;
    public ChangeScene changeScene;

    private void Start()
    {
        gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
    }
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            gameTimer.StartTimer();
            changeScene.Activate();
        }

    }
}
