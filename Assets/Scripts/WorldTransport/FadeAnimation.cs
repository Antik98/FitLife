using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnimation : MonoBehaviour
{
    public Animator Fade;

    public IEnumerator TriggerFade(float secs, params Action[] duringFade)
    {
        GameObject.FindGameObjectWithTag("Player")?.GetComponent<playerMovement>()?.lockPlayer();
        Fade.SetTrigger("Start");
        yield return new WaitForSeconds(secs-0.5f);
        foreach (Action x in duringFade)
            x();

        Fade.SetBool("ForcedStart", true);
        yield return new WaitForSeconds(0.5f);
        Fade.SetBool("ForcedStart", false);
        GameObject.FindGameObjectWithTag("Player")?.GetComponent<playerMovement>()?.unlockPlayer();
    }
}
