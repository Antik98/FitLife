using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundEffectScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip objectShow;
    public AudioClip objectHide;

    private IEnumerator Start()
    {
        audioSource.mute = true;
        yield return new WaitForSeconds(0.3f);
        audioSource.mute = false;
    }

    public void MakeOpenSound()
    {
        audioSource?.PlayOneShot(objectShow, 0.2f);
    }

    public void MakeCloseSound()
    {
        audioSource?.PlayOneShot(objectHide, 0.2f);
    }

}
