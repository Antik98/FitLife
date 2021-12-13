using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WordGenerator : MonoBehaviour
{
    private static int index = 0;
    private static string[] wordList = {   "alkohol" , "okruh" , "obvod" , "grafika" , "vinotéka" ,
                                    "zabezpečení" , "útok" , "vodka" , "mapa" , "obvod" ,
                                    "seřazení" , "potvrzení" , "myška" , "programátor" , "proměnná" ,
                                    "strom" , "inkvizice" , "pole" , "počítadlo" , "léky" ,
                                    "obrazovka" , "deska" , "škola" , "počítač" , "pero" , 
                                    "pivo" , "náhoda" , "hra" , "ústav" , "lednička" };
    public static string GetRandomWord()
    {
        string randomWord = wordList[index];
        index++;

        if (index >= 30)
            index = 0;

        return randomWord;
    }
}
