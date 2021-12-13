using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawner : MonoBehaviour
{
	[SerializeField]
	private GameObject characterPrefab = default;
	[SerializeField]
	private float respawnTime = 1f;
	private float nextSpawnTime = 1f;
	private int bulletDir = 3;

    void Update()
    {
        if ( Time.time >= nextSpawnTime ) {
			nextSpawnTime = Time.time + respawnTime;
			for ( int i = 0; i < 3; i++ ) {
				GameObject tmpObj = Instantiate(characterPrefab, transform.position, transform.rotation);
				if ( i == 0 ) {tmpObj.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletDir, 0);}
				if ( i == 1 ) {tmpObj.GetComponent<Rigidbody2D>().velocity = new Vector2( bulletDir, 0);}
			}
		}
    }
}
