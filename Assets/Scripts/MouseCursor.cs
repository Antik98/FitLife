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
        Vector3 cursor = Input.mousePosition;
        cursor.z = Camera.main.nearClipPlane;
        cursor = Camera.main.ScreenToWorldPoint(cursor);
        transform.position = cursor;
    }
}
