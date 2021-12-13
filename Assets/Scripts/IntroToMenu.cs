using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroToMenu : MonoBehaviour
{

    public float wait_time = 5f;
    private Animator transition;
    public float transition_time = 1.5f;

    void Start()
    {
        var fade = GameObject.Find("Fade");
        if (fade ?? false)
            transition = fade.GetComponent<Animator>();
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(wait_time);
        if (transition ?? false)
            transition.SetTrigger("Start");
        yield return new WaitForSeconds(transition_time);
        SceneManager.LoadScene(1);
    }
}
