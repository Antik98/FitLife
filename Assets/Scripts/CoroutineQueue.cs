using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CoroutineQueue : MonoBehaviour
{
    public List<WaitingForSceneEvent> list;

    public delegate bool WaitingForSceneEvent(string sceneName);
    public event WaitingForSceneEvent OnSceneChange;
    private void Start()
    {
        list = new List<WaitingForSceneEvent>();
    }

    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());
    }

    private IEnumerator OnEnableCoroutine()
    {
        yield return new WaitUntil(() => StatusController.initialized);
        OnSceneChange += CheckWaiting;
        SceneManager.activeSceneChanged += CheckWaiting;
    }

    private void OnDisable()
    {
        OnSceneChange += CheckWaiting;
    }

    public void TriggerSceneChanged(string scene) => OnSceneChange?.Invoke(scene);

    bool CheckWaiting(string sceneName)
    {
        list.RemoveAll(x => x(sceneName) == true);
        return true;
    }
    private void CheckWaiting(Scene current, Scene next)
    {
        TriggerSceneChanged(next.name);
        list.RemoveAll(x => x(next.name) == true);
    }

    public void Reset()
    {
        list.Clear();
    }
}
