using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerZMA : MonoBehaviour
{
	float currentTime = 0f;
	public float startingTime = 60f;
	private int minute;
	private int second;
	[SerializeField]
	private GameManagerZMA gameManager;
	void Start()
	{
		currentTime = startingTime;
		minute = (int)currentTime / 60;
		second = (int)currentTime % 60;
		GameObject go = GameObject.Find("GameManager");
		gameManager = (GameManagerZMA)go.GetComponent(typeof(GameManagerZMA));
	}

	void Update()
	{
		if (minute == 0 && second == 0)
		{
			gameManager.GameWin();
		}

		else
		{
			currentTime -= 1 * Time.deltaTime;
			minute = (int)currentTime / 60;
			second = (int)currentTime % 60;
		}
	}

}
