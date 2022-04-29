using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Toggle musicToggle;

    public void PlayMusic(bool val)
    {
        if(StatusController.initialized)
            StatusController.Instance.musicSource.mute = !val;
    }
    public void Start()
    {
        if (musicToggle != null && StatusController.initialized)
            musicToggle.isOn = !StatusController.Instance.musicSource.mute;
    }
}
