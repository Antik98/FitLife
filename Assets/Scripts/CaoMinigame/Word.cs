using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word
{
    public string word;
    private int activeChar;
    private WordDisplay display;

    public Word(string word, WordDisplay display)
    {
        this.word = word;
        this.activeChar = 0;
        this.display = display;
        this.display.SetWord(word);
    }

    public char GetNextLetter()
    {
        return word[activeChar];
    }

    public void TypeLetter()
    {
        activeChar++;
        display.RemoveLetter();
    }

    public void ChangeColor()
    {
        display.ChangeColor();
    }

    public void DeleteWord()
    {
            display.RemoveWord();
    }

    public bool IsCompleted()
    {
        bool typed = activeChar >= word.Length;
        if (typed)
        {
            display.RemoveWord();
        }

        return typed;
    }
}
