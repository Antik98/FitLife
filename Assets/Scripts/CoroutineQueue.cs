using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CoroutineQueue : MonoBehaviour
{
    public List<WaitingForSceneEvent> list = new List<WaitingForSceneEvent>();

    public delegate bool WaitingForSceneEvent(string sceneName);
    public event WaitingForSceneEvent OnSceneChange;

    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());
    }

    private IEnumerator OnEnableCoroutine()
    {
        yield return new WaitUntil(() => StatusController.initialized);
        SceneManager.activeSceneChanged += CheckWaiting;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= CheckWaiting;
    }

    public void TriggerSceneChanged(string scene)
    {
        return;
    }

    private void CheckWaiting(Scene current, Scene next)
    {
        list.RemoveAll(x => x(next.name) == true);
        TriggerSceneChanged(next.name);
    }

    public void Reset()
    {
        list.Clear();
    }
}
