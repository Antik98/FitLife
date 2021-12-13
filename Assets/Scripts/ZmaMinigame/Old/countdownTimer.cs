using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdownTimer : MonoBehaviour
{
    float currentTime = 0f;
	public float startingTime = 10f;
	private int minute;
	private int second;
	public Text displayTime;
	public GameObject endgameText;

    void Start()
	{
        currentTime = startingTime;
		minute = (int)currentTime / 60;
		second = (int)currentTime % 60;
		// endgameText = GameObject.Find("endgame");	
		//	doesnt work? - error nullreferenceexception: object reference not set to an instance of an object
		// had to set public. But it works fine in script "health.cs"
		endgameText.SetActive(false);
    }

    void Update()
    {
		if ( minute == 0 && second == 0 ) {
			endgameText.GetComponent<Text>().text = "ČAS VYPRŠEL";
			endgameText.SetActive(true);
			Time.timeScale = 0;
		}
		else {
			currentTime -= 1 * Time.deltaTime;
			minute = (int)currentTime / 60;
			second = (int)currentTime % 60;
			displayTime.text = minute.ToString("00") + ":" + second.ToString("00");
		}
    }
}
