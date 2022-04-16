using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public Dialogue(string sentence)
    {
        sentences = new string[] { sentence };
    }
    public override int GetHashCode()
    {
        return sentences.Aggregate(1, (prod, next) => prod ^ next.GetHashCode());
    }
}
