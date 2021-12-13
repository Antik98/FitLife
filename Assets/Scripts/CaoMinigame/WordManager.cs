using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public List<Word> words;
    private bool hasActiveWord;
    private Word activeWord;
    public GameManager gameManager;
    public WordSpawner wordSpawner;
    private int completedWords;
    public bool completed;

    private void Start()
    {
        completed = false;
        completedWords = 0;
    }

    public void AddWord()
    {
        Word word = new Word(WordGenerator.GetRandomWord(), wordSpawner.SpawnWord());
        words.Add(word);
    }

    public void TypeLetter(char letter)
    {
        if (hasActiveWord)
        {
            if (activeWord.GetNextLetter() == letter)
            {
                activeWord.TypeLetter();
            }

            else
            {
                activeWord.ChangeColor();
            }
        }

        else
        {
            foreach (Word word in words)
            {
                if (word.GetNextLetter() == letter)
                {
                    activeWord = word;
                    hasActiveWord = true;
                    word.TypeLetter();
                    break;
                }

                else
                {
                    activeWord = word;
                    hasActiveWord = true;
                    word.ChangeColor();
                    break;
                }
            }
        }


        if (hasActiveWord && activeWord.IsCompleted())
        {
            hasActiveWord = false;
            words.Remove(activeWord);
            completedWords++;
            if (completedWords > 27 && !completed)
                gameManager.GameWin();
        }
    }


    public bool DeleteWord()
    {
        if (words.Count == 0)
            return true;

        words[0].DeleteWord();
        words.RemoveAt(0);
        hasActiveWord = false;
        return true;
    }

    public int CompletedWords()
    {
        return completedWords;
    }
}
