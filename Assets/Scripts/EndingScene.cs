using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScene : MonoBehaviour
{
    public Text energyStat;
    public Text socialStat;
    public Text hungerStat;
    public Text questStat;
    public Text easterEggsFoundStat;
    public PopUpMessage popUpMessage;
    PlayerStatus playerStatus;
    public Sprite lastSpeaking;

    // Start is called before the first frame update
    void Start()
    {
        var statusController = StatusController.Instance;

        if((statusController ?? false))
        {
            playerStatus = statusController.GetComponent<PlayerStatus>();
            energyStat.text = playerStatus.energy.ToString();
            socialStat.text = playerStatus.social.ToString();
            hungerStat.text = playerStatus.hunger.ToString();
            //TODO dirty hack
            questStat.text = string.Format("{0}/{1}", statusController.GetComponent<QuestTracker>().getQuestsDone(), 16);// QuestTracker.totalQuestsStatic);
            easterEggsFoundStat.text = playerStatus.foundEasterEggs.ToString();

            StartCoroutine(showDialog());
            


        }
    }
    IEnumerator showDialog()
    {
        if (playerStatus.energy <= 0)
        {
            yield return new WaitForSeconds(0.5f);
            string[] _tmp = { "Málo jsi spal. Po zkouškách jsi zkolaboval, jsi nyní v kómatu" };
            popUpMessage.Open(new Dialogue(_tmp), lastSpeaking);
            Time.timeScale = 1f;
        }
        if (playerStatus.hunger <= 0)
        {
            yield return new WaitForSeconds(0.5f);
            string[] _tmp = { "Když tě viděla babička celého vyhublého o Vánocích, dostala infarkt.","Naštěstí to přežila, ale musel jsi sníst celého kapra sám, abys ujistil babičku, že ještě žiješ" };
            popUpMessage.Open(new Dialogue(_tmp), lastSpeaking);
            Time.timeScale = 1f;
        }
        if (playerStatus.social <= 0)
        {
            yield return new WaitForSeconds(0.5f);
            string[] _tmp = { "Ztratil jsi většinu svých kamarádů, ani si nepamatuješ jména svých rodičů." };
            popUpMessage.Open(new Dialogue(_tmp), lastSpeaking);
            Time.timeScale = 1f;
        }
        yield return new WaitForSeconds(1);
    }

}
