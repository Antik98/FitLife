using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kavodaMoving : MonoBehaviour
{
    private float speed;
    private bool moveRight;
	private Coroutine coroutine;
	private GameObject kalvoda;
	private float distance;

	private void Start() {
		kalvoda = GameObject.Find("kalvoda 64");
		speed = Random.Range(2,6);
		distance = getDistance();
		coroutine = StartCoroutine(ExecuteAfterTime(Random.Range(distance /(speed * 12), distance / (speed * 2)))); 
	}

	private float getDistance() {
		float distance;
		if (moveRight)	distance = GameObject.Find("turn_right").transform.position.x - kalvoda.transform.position.x;
		else			distance = kalvoda.transform.position.x - GameObject.Find("turn_left").transform.position.x;
		return distance;
	}
    void Update()
    {
		if (moveRight) 	transform.Translate( 2 * speed * Time.deltaTime, 0, 0);
		else 			transform.Translate(-2 * speed * Time.deltaTime, 0, 0);
    }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("turn")) {
			moveRight = !moveRight;
			float distance = getDistance();
			float sec = Random.Range(distance / (12 * speed), distance / (2 * speed));
			StopCoroutine(coroutine);
			coroutine = StartCoroutine(ExecuteAfterTime(sec));
		}
	}

	 IEnumerator ExecuteAfterTime(float time)
	{
		while(true){
			// random time to switch side
			yield return new WaitForSeconds(time);
			moveRight = !moveRight;
			speed = Random.Range(2,6);
			float distance = getDistance();
			time = Random.Range(distance /(speed * 12), distance / (speed * 2));
		}
		
	}
}



