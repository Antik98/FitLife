using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDayEndEvent : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());

    }

    IEnumerator OnEnableCoroutine()
    {
        yield return new WaitUntil(() => StatusController.initialized);
        StatusController.Instance.gameTimer.BroadcastDayPassed += HandleDayEnd;
    }
    void OnDisable()
    {
        StatusController.Instance.gameTimer.BroadcastDayPassed -= HandleDayEnd;
    }

    private void HandleDayEnd()
    {
        if (SceneManager.GetActiveScene().name != "HomeScene")
        {
            StatusController.Instance.coroutineQueue.list.Add((sceneName) => WaitForPlayerToBeHome(sceneName));
            StartCoroutine(GameObject.FindGameObjectWithTag("Transition")?.GetComponent<FadeAnimation>().TriggerFade(5, "HomeScene", "Další den..."));
        }
        else
        {
            StartCoroutine(GameObject.FindGameObjectWithTag("Transition")?.GetComponent<FadeAnimation>().TriggerFade(5, "HomeScene", "Další den..."));
        }
    }

    private bool WaitForPlayerToBeHome(string sceneName)
    {
        if(sceneName == "HomeScene")
        {
            
            GameObject gameController = GameObject.Find("UI");
            PopUpMessage popupMessage = gameController.GetComponent<PopUpMessage>();
            Sprite QuestIcon = Resources.LoadAll<Sprite>("PopUpMessageIcons")[0];
            popupMessage.Open(new Dialogue("Ufff to mě bolí hlava, měl jsem se jít vyspat a tolik neponocovat..."), QuestIcon);
            StatusController.Instance.PlayerStatus.addStatValues(-20,-20,-20);
            return true;
        }
        return false;
    }
}