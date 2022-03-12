using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationScript : MonoBehaviour
{
    public GameObject questMenu;
    public PhoneDisplay phone;

    public void OpenNotifications()
    {
        if(!questMenu.activeSelf)
        {
            questMenu.SetActive(true);
            if (phone.isActive())
            {
                phone.SetPhoneState(false);
            }
        }
    }

    public void CloseNotification()
    {
        questMenu.SetActive(false);

    }
 
}
