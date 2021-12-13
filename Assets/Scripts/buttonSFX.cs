using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSFX : MonoBehaviour
{
    AudioSource audioPlayer;
	public AudioClip hover;
	public AudioClip click;

	private void Start() {
		audioPlayer = GetComponent<AudioSource>();
	}
	
	public void HoverSound() {
		audioPlayer.PlayOneShot(hover);
	}

	public void ClickSound() {
		audioPlayer.PlayOneShot(click);
	}

}
