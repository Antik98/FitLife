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
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = Input.mousePosition;
    }
}
