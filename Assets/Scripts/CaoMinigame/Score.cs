using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private int curLives = 3;
    private WordManager wordManager;
    private void Start()
    {
        wordManager = FindObjectOfType<WordManager>();
    }

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < curLives)
                hearts[i].sprite = fullHeart;
            else hearts[i].sprite = emptyHeart;
        }

        scoreText.text = wordManager.CompletedWords().ToString();

    }

    public void Decrease()
    {
        curLives--;
        if( curLives == 0 )
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
