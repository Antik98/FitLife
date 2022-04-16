using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPopUp : MonoBehaviour
{

    public GameObject popUp;
    public TextMeshProUGUI text;

    private void Start()
    {
        popUp.SetActive(false);
        StatusController.Instance.interactionTracker.HandleEventViewHint += TriggerView;
    }

    private void OnDisable()
    {
        StatusController.Instance.interactionTracker.HandleEventViewHint -= TriggerView;
    }
    public void TriggerView(object sender, bool enable, string value)
    {
        if (enable && !string.IsNullOrWhiteSpace(value))
        {
            popUp.SetActive(true);
            text.text = value;
        }
        else
        {
            popUp.SetActive(false);
        }
    }
}
