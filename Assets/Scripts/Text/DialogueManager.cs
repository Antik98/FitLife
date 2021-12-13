using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;

    public Queue<string> dialogueQueue;

    // Start is called before the first frame update
    void Start()
    {
        dialogueQueue = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueQueue.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            dialogueQueue.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string display = dialogueQueue.Dequeue();
        Debug.Log(display);
        dialogueText.text = display;
    }

    public void EndDialogue()
    {
        Debug.Log("ending dialogue");
        dialogueText.text = "";
    }
}
