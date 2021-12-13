using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoving : MonoBehaviour
{
	public float speed;
	private float xLimitLeft;
	private float xLimitRight;
	void Start() {
		xLimitLeft = -6;
		xLimitRight = 6;
	}

    void Update()
    {
		if (Input.GetKey(KeyCode.RightArrow)) 
			transform.Translate( 2 * speed * Time.deltaTime, 0 , 0 );
		if (Input.GetKey(KeyCode.LeftArrow)) 
			transform.Translate(-2 * speed * Time.deltaTime, 0 , 0 );
		if (transform.position.x < xLimitLeft || transform.position.x > xLimitRight)
			transform.position = new Vector2( Mathf.Clamp( transform.position.x + Time.deltaTime * speed, xLimitLeft, xLimitRight), transform.position.y );
	}
}
