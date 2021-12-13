using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordTimer : MonoBehaviour
{
    public WordManager wordManager;

    public float wordDelay = 1.5f;
    public float maxWords = 0;
    public float endGameDelay = 5f;
    private float nextWordTime = 0f;
    private float remWords = 0;

    private void Start()
    {
        remWords = maxWords;
    }

    private void FixedUpdate()
    {
        if (Time.time >= nextWordTime && remWords > 0 )
        {
            wordManager.AddWord();
            remWords--;
            nextWordTime = Time.time + wordDelay;
            wordDelay *= 0.99f;
        }


    }
}
