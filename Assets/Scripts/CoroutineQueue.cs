using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineQueue : MonoBehaviour
{
    public List<WaitingForSceneEvent> list;

    public delegate bool WaitingForSceneEvent(string sceneName);
    public static event WaitingForSceneEvent OnSceneChange;
    private void Start()
    {
        list = new List<WaitingForSceneEvent>();
    }

    private void OnEnable()
    {
        OnSceneChange += CheckWaiting;
    }

    private void OnDisable()
    {
        OnSceneChange += CheckWaiting;
    }

    public void TriggerSceneChanged(string scene) => CheckWaiting(scene);

    bool CheckWaiting(string sceneName)
    {
        list.RemoveAll(x => x(sceneName) == true);
        return true;
    }
}
