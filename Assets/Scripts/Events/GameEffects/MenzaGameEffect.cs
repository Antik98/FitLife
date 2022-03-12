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
        GameTimer _gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        PlayerStatus _playerStatus= GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
        if (_gameTimer.gameTime.Hours < 19)
        {
            yield return fade.TriggerFade(3);
            _playerStatus.addHungerValue(hungerAdded);
            yield return new WaitForSeconds(1);
            _playerStatus.addEnergyValue(energyAdded);
            _gameTimer.SleepHours(1f);
        }
        else
        {
            done = true;
        }
    }
}

