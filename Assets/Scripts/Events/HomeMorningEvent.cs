using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeMorningEvent : MonoBehaviour
{
    public GameObject controller;
    public GameObject UI;
    public string requiredPrevScene;
    public Dialogue dialogue;
	public bool isFinished;
	private bool dialogEnabled;
	private int keyCnt;
    void Start()
    {
	    var isUnitTest = System.Environment.GetEnvironmentVariable("isUnityTestRunning") == "true";
		keyCnt = 0;
		isFinished = false;
		dialogEnabled = false;
        if(requiredPrevScene == SceneController.prevScene && !isUnitTest)
        {
            StartCoroutine(DisplayText());
        } 
    }

	private void Update() {
		if ( dialogEnabled && (keyCnt < 6) && Input.GetKeyDown(KeyCode.Space) )
				keyCnt++;
		if ( keyCnt == 6 ) {
			isFinished = true;	
			keyCnt++;
		}
	}
    IEnumerator DisplayText()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        UI.GetComponent<PopUpMessage>().Open(dialogue);
		dialogEnabled = true;
    }
}
