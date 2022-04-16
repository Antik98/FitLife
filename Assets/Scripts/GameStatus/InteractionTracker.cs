using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTracker : MonoBehaviour
{
    public int easterEggsFound { get; private set; }

    private Dictionary<int, bool> interactions = new Dictionary<int, bool>();

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
    }
    private void OnDisable()
    {
        StatusController.Instance.gameTimer.BroadcastDayPassed -= HandleDayPassed;
    }

    private void HandleDayPassed()
    {
        interactions.Clear();
    }

    public void TriggerHint(object sender, bool view, string hint = "")
    {
        HandleEventViewHint?.Invoke(sender, view, hint);
    }

    public bool isInteractionAvailable(int id)
    {
        return interactions.ContainsKey(id) && interactions[id];
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
