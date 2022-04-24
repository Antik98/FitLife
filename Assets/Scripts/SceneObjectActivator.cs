using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneObjectActivator : MonoBehaviour
{
    public GameObject[] ObjectsToTurnOn;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());

    }

    IEnumerator OnEnableCoroutine()
    {
        yield return new WaitUntil(() => StatusController.initialized);
        GameTimer _gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        var currentDay = _gameTimer.gameTime.Days;

        ObjectsToTurnOn.ToList().ForEach(s => s.SetActive(false));
        ObjectsToTurnOn.ElementAtOrDefault(currentDay).SetActive(true);
    }

}
