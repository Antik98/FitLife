using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PopUpMessage : MonoBehaviour
{
    public GameObject ui;
    public Queue<string> dialogueQueue;
    Text textObject;

    private Vector3 mePosition;
    private Vector3 theyPosition;
    private GameObject player;
    private bool setPosition = false;
    public Sprite Player;
    private Image imgObject;
    private Sprite optionalSprite;
    private Sprite optionalSprite2;
    private Sprite optionalSprite3;

    void Start()
    {
        dialogueQueue = new Queue<string>();
        textObject = ui.gameObject.GetComponentInChildren<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        imgObject = ui.gameObject.GetComponentsInChildren<Image>()[3];

    }

    void Update()
    {
        if(ui.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }

    }

    public void Open(Dialogue dialogue, Sprite optionalSprite = null, Sprite optionalSprite2 = null, Sprite optionalSprite3 = null)
    {

        this.optionalSprite = optionalSprite;
        this.optionalSprite2 = optionalSprite2;
        this.optionalSprite3 = optionalSprite3;

        ui.SetActive(true);

        if (player ?? false)
            player.GetComponent<playerMovement>().lockPlayer();

        DisplayText(dialogue);

        Time.timeScale = 0f;
    }

    public void DisplayText(Dialogue dialogue)
    {
        dialogueQueue.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            dialogueQueue.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (!setPosition)
        {
            mePosition = ui.GetComponent<RectTransform>().position;
            theyPosition = mePosition;
            theyPosition.x += 2.3f;
            setPosition = true;
        }
       
        if (dialogueQueue.Count == 0)
        {
            Close();
            return;
        }

        string display = dialogueQueue.Dequeue();
  
        
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
            if (optionalSprite != null)
            {
                imgObject.sprite = optionalSprite;
            }
            else
                imgObject.sprite = null;

           
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
        return ui.activeSelf;
    }
    public bool isNotActive()
    {
        return !ui.activeSelf;
    }
    public void Close()
    {
        textObject.text = "";
        ui.SetActive(!ui.activeSelf);
        if (!ui.activeSelf)
        {
            Time.timeScale = 1f;
        }

        foreach (Transform child in transform)
        {
            //child.position = mePosition;
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