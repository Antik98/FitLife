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
    float defaultAlpha;
    
    void Start()
    {
        sprites = GameObject.FindGameObjectsWithTag("DayNightSprites").ToList();
        gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        defaultAlpha = sprites?.FirstOrDefault()?.GetComponent<TilemapRenderer>().material.color.a ?? 1;

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
                s.GetComponent<TilemapRenderer>().material.color = new Color(s.GetComponent<TilemapRenderer>().material.color.r, s.GetComponent<TilemapRenderer>().material.color.g, s.GetComponent<TilemapRenderer>().material.color.b, 0);
                s.SetActive(false);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        // sprites = GameObject.FindGameObjectsWithTag("DayNightSprites").ToList();
        // Debug.Log("Updating day night for " + sprites.Count);
        if (gameTimer.gameTime.Hours >= 19)
        {
            sprites.Where(s => s.GetComponent<TilemapRenderer>().material.color.a <= 0f).ToList().ForEach(s =>
            {
                s.SetActive(true);
                StartCoroutine(FadeIn(s.GetComponent<TilemapRenderer>()));
            });
        }

        else if (gameTimer.gameTime.Hours >= 4 && gameTimer.gameTime.Hours < 19)
        {
            sprites.Where(s => s.activeSelf && s.GetComponent<TilemapRenderer>().material.color.a > 0f).ToList().ForEach(s =>
            {
                s.SetActive(true);
                StartCoroutine(Fade(s.GetComponent<TilemapRenderer>()));
            });
        }
    }
    
    IEnumerator Fade(Renderer renderer) 
    {
        for (float ft = renderer.material.color.a; ft > 0f; ft -= 0.1f) 
        {
            Color c = renderer.material.color;
            c.a = ft;
            renderer.material.color = c;
            yield return new WaitForSeconds(.5f);
        }
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0);
    }
    
    IEnumerator FadeIn(Renderer renderer)
    {
        var tmp = defaultAlpha; 
        for (float ft = 0; ft <= tmp; ft += 0.1f) 
        {
            Color c = renderer.material.color;
            c.a = ft;
            renderer.material.color = c;
            yield return new WaitForSeconds(.5f);
        }
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, defaultAlpha);
    }

}
