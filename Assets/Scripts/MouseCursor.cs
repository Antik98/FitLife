using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Cursor.visible = false;
        Vector3 mouseposition = Input.mousePosition;
        mouseposition.z = Camera.main.nearClipPlane;
        mouseposition = Camera.main.ScreenToWorldPoint(mouseposition);
        transform.position = mouseposition;
        /*
        Vector3 cursor = new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, 0);
        cursor = Camera.main.ScreenToWorldPoint(cursor);
        cursor.z = 0;
        Debug.Log(cursor);
        transform.position = cursor; //Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Debug.Log(transform.position);
        */
        /*
        Vector3 mouseWzPos = new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, Camera.main.transform.position.z);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseWzPos);

        mousePos.z = 0;
        Debug.Log(mousePos);
        transform.position = mousePos;
        */
    }
}
