using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DayNightManager : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> sprites;
    private GameTimer gameTimer; 
    
    void Start()
    {
        sprites = GameObject.FindGameObjectsWithTag("DayNightSprites").ToList();
        gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        if (gameTimer.gameTime.Hours >= 19 || gameTimer.gameTime.Hours < 4)
        {
            sprites.ForEach(s =>
            {
                s.SetActive(true);
            });
        }
        else
        {
            sprites.ForEach(s =>
            {
                s.SetActive(false);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        // sprites = GameObject.FindGameObjectsWithTag("DayNightSprites").ToList();
        // Debug.Log("Updating day night for " + sprites.Count);
        if (gameTimer.gameTime.Hours == 18 && gameTimer.gameTime.Minutes > 56)
        {
            sprites.ForEach(s =>
            {
                s.SetActive(true);
                StartCoroutine(FadeIn(s.GetComponent<TilemapRenderer>()));
            });
        }

        if (gameTimer.gameTime.Hours == 3 && gameTimer.gameTime.Minutes > 56)
        {
            sprites.ForEach(s =>
            {
                s.SetActive(true);
                StartCoroutine(Fade(s.GetComponent<TilemapRenderer>()));
            });
        }
    }
    
    IEnumerator Fade(Renderer renderer) 
    {
        for (float ft = renderer.material.color.a; ft >= 0; ft -= 0.1f) 
        {
            Color c = renderer.material.color;
            c.a = ft;
            renderer.material.color = c;
            yield return new WaitForSeconds(.5f);
        }
    }
    
    IEnumerator FadeIn(Renderer renderer)
    {
        var tmp = renderer.material.color.a; 
        for (float ft = 0; ft < tmp; ft += 0.1f) 
        {
            Color c = renderer.material.color;
            c.a = ft;
            renderer.material.color = c;
            yield return new WaitForSeconds(.5f);
        }
    }

}
