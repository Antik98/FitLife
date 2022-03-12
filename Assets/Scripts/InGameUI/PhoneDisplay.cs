using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneDisplay : MonoBehaviour
{
    public GameObject ui;
    public GameObject pauseMenu;
    public Animator phoneAnimator;
    private PlayerStatus playerStatus;


    // Update is called once per frame
    private void Start()
    {
        ui.SetActive(true);
        playerStatus = StatusController.Instance.GetComponent<PlayerStatus>();
    }

    public void SetPhoneState(bool state)
    {
        phoneAnimator.SetBool("IsOpen", state);
    }

    public bool isActive()
    {
        return phoneAnimator.GetBool("IsOpen");
    }
    void Update()
    {
        if (!pauseMenu.activeSelf){
           if(Input.GetKeyDown(KeyCode.K) && !playerStatus.doTutorial)
            {
                phoneAnimator.SetBool("IsOpen", !isActive());
            }
        }
    }

}
