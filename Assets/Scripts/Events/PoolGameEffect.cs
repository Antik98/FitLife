using System.Collections;
using UnityEngine;

public class PoolGameEffect : GameEffect
{
    public FadeAnimation fadeAnimation;
    public void Start()
    {
        done = false;
    }
    public override IEnumerator execute()
    {
        GameTimer _gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        PlayerStatus _playerStatus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
        if (_gameTimer.gameTime.Hours < 19)
        {
            yield return fadeAnimation.TriggerFade(2.5f);
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
