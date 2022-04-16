using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenzaGameEffect : GameEffect
{
    public FadeAnimation fade;
    public int hungerAdded;
    public int energyAdded;
    public void Start()
    {
        done = false;
    }
    public override IEnumerator execute()
    {
        yield return null;
        GameTimer _gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        PlayerStatus _playerStatus= GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
        if (_gameTimer.gameTime.Hours < 19)
        {
            yield return fade.TriggerFade(3, "O 30 minut později...");
            _playerStatus.addStatValues(energyAdded, 0, hungerAdded);
            _gameTimer.SleepHours(0.5f);
        }
        done = true;
    }
}

