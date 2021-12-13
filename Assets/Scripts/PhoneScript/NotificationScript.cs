using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationScript : MonoBehaviour
{
    public GameObject questMenu;
    public GameObject phone;

    public void OpenNotifications()
    {
        if(!questMenu.activeSelf)
        {
            questMenu.SetActive(true);
            if (phone.activeSelf)
            {
                phone.SetActive(false);
            }
        }
    }

    public void CloseNotification()
    {
        questMenu.SetActive(false);

    }
 
}
