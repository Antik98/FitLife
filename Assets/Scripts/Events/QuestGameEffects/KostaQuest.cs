using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KostaQuest : GameEffect
{
    public Sprite sprite;
    public void Start()
    {
        execute();
    }
    public override IEnumerator execute()
    {
        QuestTracker _questTracker = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
        if (_questTracker.getQuest(10).status == Quest.Status.completed)
        {
            GameObject gameController = GameObject.Find("UI");
            PopUpMessage _popupMessage = gameController.GetComponent<PopUpMessage>();
            string[] _tmp = { "Bráško, ty tzatziki jsou pro mě? Oukej, odteď jsi členem klubu Fit--." };
            _popupMessage.Open(new Dialogue(_tmp), sprite);
            GameObject.Find("Filip").transform.position= new Vector3(1.8f, -0.198f, 0);
        }
        yield return new WaitForSeconds(0);
    }

}
