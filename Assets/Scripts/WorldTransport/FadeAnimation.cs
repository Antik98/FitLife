using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeAnimation : MonoBehaviour
{
    public Animator Fade;

    public TextMeshProUGUI fadeTextBox;

    public ChangeScene changeScene;

    public IEnumerator TriggerFade(float secs, string fadeText = "", params Action[] duringFade)
    {
        StatusController.Instance.interactionTracker.TriggerHint(this, false);
        GameObject.FindGameObjectWithTag("Player")?.GetComponent<playerMovement>()?.lockPlayer();
        Fade.SetTrigger("Start");

        if(fadeText != null)
            fadeTextBox.text = fadeText;

        yield return new WaitForSeconds(secs-0.5f);
        foreach (Action x in duringFade)
            x();

        Fade.SetBool("ForcedStart", true);
        yield return new WaitForSeconds(0.5f);
        Fade.SetBool("ForcedStart", false);
        if (fadeText != null)
            fadeTextBox.text = "";
        GameObject.FindGameObjectWithTag("Player")?.GetComponent<playerMovement>()?.unlockPlayer();
    }

    //used when fadeText is needed and transport, fadout disabled
    public IEnumerator TriggerFade(float secs, string transportToScene, string fadeText = "", params Action[] duringFade)
    {
        StatusController.Instance.interactionTracker.TriggerHint(this, false);
        GameObject.FindGameObjectWithTag("Player")?.GetComponent<playerMovement>()?.lockPlayer();
        Fade.SetTrigger("Start");

        if (fadeText != null)
            fadeTextBox.text = fadeText;

        yield return new WaitForSeconds(secs);
        foreach (Action x in duringFade)
            x();
        if (fadeText != null)
            fadeTextBox.text = "";
        GameObject.FindGameObjectWithTag("Player")?.GetComponent<playerMovement>()?.unlockPlayer();
        changeScene.Activate(transportToScene);
    }
}
