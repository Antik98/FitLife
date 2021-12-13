using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string objectName;

    [TextArea(1, 10)]
    public string[] sentences;
    public Dialogue(string[] sentences)
    {
        this.sentences = sentences;
    }
}
