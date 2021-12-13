using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCollision : MonoBehaviour
{
    public Score score;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        score.Decrease();
        FindObjectOfType<WordManager>().DeleteWord();
    }
}
