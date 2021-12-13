using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public Animator animator;
	public int currentHealth;
	public int maxHealth = 3;

	public Image[] hearts;
	public Sprite fullHeart;
	public Sprite emptyHeart;

	private GameObject endgameText;

    void Start()
    {
        currentHealth = maxHealth;
		endgameText = GameObject.Find("endgame"); // 
		endgameText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		// display hearts
		for ( int i = 0; i < hearts.Length; i++) {
			if ( i < currentHealth )	 
				hearts[i].sprite = fullHeart;
			else hearts[i].sprite = emptyHeart;
		} 
    }

	private void OnTriggerEnter2D(Collider2D other) {
		animator.SetBool("Is_hit", true);
		if ( --currentHealth == 0 ) {
			endgameText.GetComponent<Text>().text = "Vyčerpal jsi všechny pokusy";
			endgameText.SetActive(true);	
			Time.timeScale = 0;
		}
		// play animation in this time
		StartCoroutine(Wait());
	}

	IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(0.2f);
		animator.SetBool("Is_hit", false);

    }
}
