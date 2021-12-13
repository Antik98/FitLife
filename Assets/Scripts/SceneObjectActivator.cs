using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectActivator : MonoBehaviour
{
    public GameObject[] ObjectsToTurnOn;
    // Start is called before the first frame update
    void Start()
    {
        GameTimer _gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        var currentDay = _gameTimer.gameTime.Days;

        if(ObjectsToTurnOn.Length - 1 >= currentDay)
        {
            ObjectsToTurnOn[currentDay].SetActive(true);
        }else if (ObjectsToTurnOn.Length >= 1 )
        {
            ObjectsToTurnOn[ObjectsToTurnOn.Length-1].SetActive(true);
        }
    }

}
