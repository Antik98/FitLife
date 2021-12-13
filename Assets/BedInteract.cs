using System;
using InputFieldScript;
using UnityEngine;

public class BedInteract : DisplayHint
{
    // Start is called before the first frame update
    public GameObject obj;
    public GameObject popup; 

    private PlayerStatus playerStaus;
    private InputFieldManager inputFieldManager;
    private GameTimer gameTimer;
    public GameObject player;
    public int cd = 5;
    private string displayTextOnHint = "E pro Spát";
    private GameObject gameController;
    private PopUpMessage popupMessage;
    private SceneController sceneController;

    
    public  Dialogue dialogue0; 
    public  Dialogue dialogue1; 
    public  Dialogue dialogue2;
    public  Dialogue dialogueDescribe;

    private static int maxSleepingHours = 6;
    private static int energyPerHour = 5;
    private static int maxGainedEnergy = 20;


    void Start()
    {
        playerStaus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
        gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();

        sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneController>();
        inputFieldManager = obj.GetComponent<InputFieldManager>();
        gameController = GameObject.Find("UI");
        popupMessage = gameController.GetComponent<PopUpMessage> ();
        labelText = displayTextOnHint;
    }

    private void LockPlayer()
    {
        player.GetComponent<playerMovement>().lockPlayer();
    }

    public override void Action()
    {
        if (HasCollided() && Input.GetKeyDown("e"))
        {
            LockPlayer();
            Close();
            gameTimer.StopTimer();
            inputFieldManager.SetCallback((str) =>
                {
                    Close();
                    inputFieldManager.inputField.gameObject.SetActive(false);
                    // popupMessage.Open(dialogue0);

                    int hours = 0;
                    if (int.TryParse(str, out hours) && hours <= maxSleepingHours )
                    {
                        //  sleeps less than @maxSleepingHours : OK
                        sceneController.LoadScene("HomeScene");
                        gameTimer.SleepHours(hours);
                        playerStaus.energy = Math.Min(playerStaus.energy + Math.Min(hours * energyPerHour, maxGainedEnergy), 100);
                    }
                    else if (hours > maxSleepingHours)
                    {
                        //  sleeps more than @maxSleepingHours : NOT OK
                        popupMessage.Open(dialogue1);
                    }
                    else
                    {
                        // invalid input, cannot be parsed into int : NOT OK
                        popupMessage.Open(dialogue2);
                    }
                    gameTimer.StartTimer();
                    Restore();
                }
            );
            // popupMessage.Open(dialogueDescribe);

        }
    }

    private void Restore()
    {
        inputFieldManager.Restore(); 
    }


}