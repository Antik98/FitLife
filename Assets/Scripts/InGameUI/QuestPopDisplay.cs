using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPopDisplay : MonoBehaviour
{
    public GameObject pauseMenu;
    public Animator questAnimator;
    private PlayerStatus playerStatus;


    // Update is called once per frame
    private void Start()
    {
        playerStatus = StatusController.Instance.GetComponent<PlayerStatus>();
    }

    public bool isActive()
    {
        return questAnimator.GetBool("IsOpen");
    }
    void Update()
    {
        if (!pauseMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.J) && !playerStatus.doTutorial)
            {
                questAnimator.SetBool("IsOpen", !isActive());
            }
        }
    }
}
