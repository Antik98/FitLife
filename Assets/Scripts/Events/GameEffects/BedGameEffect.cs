using System;
using System.Collections;
using InputFieldScript;
using UnityEngine;

public class BedGameEffect : GameEffect
{
    private PlayerStatus playerStaus;
    private GameTimer gameTimer;
    public PopUpMessage popupMessage;

    public FadeAnimation fade;

    void Start()
    {
        playerStaus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
        gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
    }

    public override IEnumerator execute()
    {
        if (gameTimer.gameTime.Hours >= 21)
        {
            yield return fade.TriggerFade(5, gameTimer.TriggerNextDay);
        }
        else
        {
            popupMessage.Open(new Dialogue("Spát můžeš až od 21:00!"));
        }
    }
}