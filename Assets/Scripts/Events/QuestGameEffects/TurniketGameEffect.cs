using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurniketGameEffect : GameEffect
{
    public Image blackFade;
    public void Start()
    {
        blackFade.canvasRenderer.SetAlpha(0.0f);
        done = false;
    }

    public override IEnumerator execute()
    {
        blackFade.canvasRenderer.SetAlpha(0.0f);
        PlayerStatus _playerStatus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        if (_questTracker.getQuest(12).status == Quest.Status.completed)
        {
            GameTimer _gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
            if(_gameTimer.gameTime.Hours >= 16)
            {

                blackFade.CrossFadeAlpha(10, 1, false);
                yield return new WaitForSeconds(2f);
                blackFade.CrossFadeAlpha(0, 1, false);
                _playerStatus.addSocialValue(-20);
                _gameTimer.SleepHours(2f);
                if(_questTracker.getQuest(16).GetStatus() != Quest.Status.completed && _questTracker.getQuest(16).GetStatus() != Quest.Status.failed && _questTracker.getQuest(16).GetStatus() != Quest.Status.canceled)
                    this.GetComponent<ChangeScene>().Activate();
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                string[] _tmp = { "Na progtest je ještě moc brzo ¯\\_(ツ)_/¯" };
                GameObject.FindGameObjectWithTag("UI").GetComponent<PopUpMessage>().Open(new Dialogue(_tmp));
            }
            
        }
        yield return new WaitForSeconds(0);
    }
}
