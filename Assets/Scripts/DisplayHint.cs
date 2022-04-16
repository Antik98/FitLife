using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHint : MonoBehaviour
{
    protected string labelText = "";
    protected bool displayHint = true;
    protected bool hasCollided = false;

    void Update()
    {
        Action();
    }

    public virtual void Action() { }

    public void Display(string textToDisplay)
    {
        if(displayHint)
            StatusController.Instance?.interactionTracker?.TriggerHint(this, true, textToDisplay);
    }

    public void Close()
    {
        StatusController.Instance?.interactionTracker?.TriggerHint(this, false, "");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasCollided = true;
            Display(labelText);
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
