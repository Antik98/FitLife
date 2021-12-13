using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHint : MonoBehaviour
{
    protected string labelText = "";
    protected bool displayHint = false;
    protected bool hasCollided = false;

    void Update()
    {
        Action();
    }

    public virtual void Action() { }

    void OnGUI(){
        if (displayHint) {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(GameObject.FindGameObjectsWithTag("Player")[0].transform.position);
            screenPosition.y = Screen.height - screenPosition.y;
            GUIStyle hintStyle = new GUIStyle("box");
            hintStyle.fontSize = 25;
            GUI.Box(new Rect((screenPosition.x - 200/2), screenPosition.y, 300, 50), labelText, hintStyle);
        }
    }

    public void Display(string textToDisplay)
    {
        labelText = textToDisplay;
        displayHint = true;
    }

    public void Close()
    {
        displayHint = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasCollided = true;
            displayHint = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Close();
        hasCollided = false;
    }

    public bool HasCollided()
    {
        return hasCollided;
    }
}
