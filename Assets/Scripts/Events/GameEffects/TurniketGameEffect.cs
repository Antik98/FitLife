using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurniketGameEffect : GameEffect
{
    public FadeAnimation fade;
    public void Start()
    {
        done = false;
    }

    public override IEnumerator execute()
    {
        PlayerStatus _playerStatus = GameObject.FindGameObjectWithTag("StatusController").GetComponent<PlayerStatus>();
        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        if (_questTracker.getQuest(12).status == Quest.Status.completed)
        {
            GameTimer _gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
            if(_gameTimer.gameTime.Hours >= 19)
            {
                if(_questTracker.getQuest(16).IsQuestFinishable(_gameTimer.gameTime))
                {
                    yield return fade.TriggerFade(5, "NTK_game", "Tolik schodů...");
                }
                else
                {
                    GameObject.FindGameObjectWithTag("UI").GetComponent<PopUpMessage>().Open(new Dialogue("Teď mám lepší věci na práci než studovat v NTK..."));
                }
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                string[] _tmp = { "Na Progtest je moc brzo, běž si ještě užívat denního světla. Programování si nech od 19:00." };
                GameObject.FindGameObjectWithTag("UI").GetComponent<PopUpMessage>().Open(new Dialogue(_tmp));
            }
            
        }
        done = true;
        yield return null;
    }
}
