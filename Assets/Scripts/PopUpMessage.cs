using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class PopUpMessage : MonoBehaviour
{
    public GameObject ui;
    public Queue<string> sentencesQueue = new Queue<string>();
    public Queue<(Dialogue, Sprite[])> dialogueQueue = new Queue<(Dialogue, Sprite[])>();
    public TextMeshProUGUI textObject;
    public Func<bool> dismissFunc;
    public Animator popUpMessageAnimator;

    private GameObject player;
    public Sprite Player;
    public Image imgObject;
    public Sprite defaultSprite;
    private Sprite optionalSprite;
    private Sprite optionalSprite2;
    private Sprite optionalSprite3;
    private GameTimer gameTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameTimer = StatusController.Instance.GetComponent<GameTimer>();
        ui.SetActive(true);
    }

    void Update()
    {
        if (!PauseGame.isGamePaused)
        {
            dismissFunc = dismissFunc ?? (() => Input.GetKeyDown(KeyCode.Space));
            if (popUpMessageAnimator.GetBool("IsOpen") && dismissFunc.Invoke())
            {
                DisplayNextSentence();
            }
        }
    }

    public void Open(Dialogue dialogue, params Sprite[] sprites)
    {
        ui.SetActive(true);
        AddDialogue(dialogue, sprites);
        if (isActive())
            return;

        popUpMessageAnimator.SetBool("IsOpen", true);

        if (player ?? false)
            player.GetComponent<playerMovement>().lockPlayer();

        gameTimer?.StopTimer();
        DisplayNextSentence();
    }

    public void AddDialogue(Dialogue dialogue, params Sprite[] sprites)
    {
        dialogueQueue.Enqueue((dialogue, sprites));
    }

    public void DisplayDialogue(Dialogue dialogue, Sprite[] sprites)
    {
        optionalSprite = sprites.ElementAtOrDefault(0);
        optionalSprite2 = sprites.ElementAtOrDefault(1);
        optionalSprite3 = sprites.ElementAtOrDefault(2);

        foreach (string sentence in dialogue.sentences)
        {
            sentencesQueue.Enqueue(sentence);
        }
    }

    public void DisplayNextSentence()
    {
       
        if (sentencesQueue.Count == 0 && dialogueQueue.Count == 0)
        {
            gameTimer.stoped = false;
            Close();
            return;
        }
        else if (sentencesQueue.Count == 0)
        {
            (var Dialogue, var Sprites) = dialogueQueue.Dequeue();
            DisplayDialogue(Dialogue, Sprites);
        }

        string display = sentencesQueue.Dequeue();
  
        
        if (display.StartsWith("[ME]"))
        {
            imgObject.sprite = Player;
            display = display.Substring(name.IndexOf("[") + 5);
        }else if(display.StartsWith("[1]")){
            imgObject.sprite = optionalSprite;
            display = display.Substring(name.IndexOf("[") + 4);
        }
        else if(display.StartsWith("[2]")){
            imgObject.sprite = optionalSprite2;
            display = display.Substring(name.IndexOf("[") + 4);

        } else if (display.StartsWith("[3]")){
            imgObject.sprite = optionalSprite3;
            display = display.Substring(name.IndexOf("[") + 4);
        }
        else
        {
            imgObject.sprite = optionalSprite ?? defaultSprite;
           
        }
        StopAllCoroutines();
        StartCoroutine(DisplaySentence(display));
        
    }

    IEnumerator DisplaySentence(string display)
    {
        textObject.text = "";

        foreach(char letter in display.ToCharArray())
        {
            textObject.text += letter;
            yield return null;
        }

    }
    public bool isActive()
    {
        return popUpMessageAnimator.GetBool("IsOpen");
    }
    public void Close()
    {
        popUpMessageAnimator.SetBool("IsOpen", false);
        textObject.text = "";
        //ui.SetActive(!ui.activeSelf);
        if (!ui.activeSelf)
        {
            Time.timeScale = 1f;
        }

        if (player ?? false)
            player.GetComponent<playerMovement>().unlockPlayer();
    }

    //You need to have Folder Resources/InvenotryItems
    public Texture TakeInvenotryCollecition(string LoadCollectionsToInventory)
    {
        Texture loadedGO = Resources.Load("InvenotryItems/" + LoadCollectionsToInventory, typeof(Texture)) as Texture;
        return loadedGO;
    }
}