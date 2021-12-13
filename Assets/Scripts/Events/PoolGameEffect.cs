using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolGameEffect : GameEffect
{
    public Image blackFade;
    public void Start()
    {
        blackFade.canvasRenderer.SetAlpha(0.0f);
        done = false;
    }
    public override IEnumerator execute()
    {
        GameTimer _gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        blackFade.canvasRenderer.SetAlpha(0.0f);
        PlayerStatus _playerStatus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
        if (_gameTimer.gameTime.Hours < 19)
        {
            blackFade.CrossFadeAlpha(10, 1, false);
            yield return new WaitForSeconds(2f);
            blackFade.CrossFadeAlpha(0, 1, false);
            _playerStatus.addSocialValue(10);
            _gameTimer.SleepHours(1f);
        }
        else
        {
            //yield return new WaitForSeconds(0);
            done = true;
        }
    }
}
