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
            yield return fadeAnimation.TriggerFade(2.5f, "O 15 minut později...");
            _playerStatus.addStatValues(socialVal: 10);
            _gameTimer.SleepHours(0.4f);
        }
        else
        {
            GameObject gameController = GameObject.Find("UI");
            PopUpMessage popupMessage = gameController.GetComponent<PopUpMessage>();
            popupMessage.Open(new Dialogue("[ME]Už jsem moc unavený i na koupel."));
        }
        done = true;
        yield return null;
    }
}
