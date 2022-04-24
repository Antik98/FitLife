using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionTracker : MonoBehaviour
{
    public int easterEggsFound { get; private set; }

    private Dictionary<int, bool> interactions = new Dictionary<int, bool>();

    private List<(int, string)> activeInteractions = new List<(int, string)>();

    public delegate void EventTriggeredViewHint(object sender, bool view, string hint);
    public event EventTriggeredViewHint HandleEventViewHint;


    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());
    }

    private IEnumerator OnEnableCoroutine()
    {
        yield return new WaitUntil(() => StatusController.initialized);
        StatusController.Instance.gameTimer.BroadcastDayPassed += HandleDayPassed;
        StatusController.Instance.coroutineQueue.OnSceneChange += HandleChangeScene;
        activeInteractions.Clear();
    }
    private void OnDisable()
    {
        StatusController.Instance.gameTimer.BroadcastDayPassed -= HandleDayPassed;
        StatusController.Instance.coroutineQueue.OnSceneChange -= HandleChangeScene;
        activeInteractions.Clear();
    }

    private void HandleDayPassed()
    {
        interactions.Clear();
    }
    private bool HandleChangeScene(string scene)
    {
        activeInteractions.Clear();
        return true;
    }

    public void TriggerHint(object sender, bool view, string hint = "")
    {
        
        if (view)
        {
            activeInteractions.Add((sender.GetHashCode(), hint));
        }
        else
        {
            activeInteractions.RemoveAll( s => s.Item1 == sender.GetHashCode() );
        }
        HandleEventViewHint?.Invoke(sender, activeInteractions.Any(), activeInteractions.FirstOrDefault().Item2) ;
    }

    public bool isInteractionSelected(object sender)
    {
        return activeInteractions.FirstOrDefault().Item1 == sender.GetHashCode();
    }

    public bool isInteractionAvailable(int id)
    {
        return interactions.ContainsKey(id) ? !interactions[id] : true;
    }
    public void addInteractionToHistory(int id, bool isEasterEgg = false)
    {
        interactions[id] = true;
        if (isEasterEgg)
            easterEggsFound++;
    }

    public void Reset()
    {
        easterEggsFound = 0;
        interactions.Clear();
    }
}
