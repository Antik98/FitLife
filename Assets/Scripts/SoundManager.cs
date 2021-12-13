using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip walk;
    public AudioClip Wood;
	public AudioClip Grass;
    AudioSource audioPlayer;
    private GameObject player;
    public bool playWalkingSound = true;

    void Start() {
        audioPlayer = GetComponent<AudioSource>();
        if(audioPlayer??false)
        {
            audioPlayer.clip = walk;
        }
        else
        {
            playWalkingSound = false;
        }

        if(GameObject.FindGameObjectsWithTag("Player") == null)
        {
            playWalkingSound = false;
        }
	}

    void Update()
    {
        
        //Debug.Log(player.transform.position);
		if(playWalkingSound && player != null)
        {
            if ((player == null) && GameObject.FindGameObjectsWithTag("Player") != null)
            {
                player = GameObject.FindGameObjectsWithTag("Player")[0];
            }
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector3.zero);
            if (hit)
            {
                if(hit.collider.gameObject.tag == "Grass")
                {
                    audioPlayer.clip = Grass;
                }
                if(hit.collider.gameObject.tag == "Beton")
                {
                    audioPlayer.clip = walk;
                }
                if(hit.collider.gameObject.tag == "Wood")
                {
                    audioPlayer.clip = Wood;
                }
            }
            
            if ((Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) && !audioPlayer.isPlaying)
            {
                audioPlayer.Play();
            }
            else if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical") && audioPlayer.isPlaying)
            {
                audioPlayer.Stop();
            }
        }
    }
}
