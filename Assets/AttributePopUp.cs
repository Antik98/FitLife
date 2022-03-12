using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AttributePopUp : MonoBehaviour
{

    public Animator attributeAnimator;
    public Sprite[] attributeImages;

    public Image image;
    public TextMeshProUGUI text;

    private PlayerStatus playerStatus;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        playerStatus = StatusController.Instance.GetComponent<PlayerStatus>();
        playerStatus.HandleEventTriggeredAttributeChanged += TriggerAttributePopUp;
    }

    private void OnEnable()
    {
        if (playerStatus != null)
            playerStatus.HandleEventTriggeredAttributeChanged += TriggerAttributePopUp;
    }

    private void OnDisable()
    {
        if (playerStatus != null)
            playerStatus.HandleEventTriggeredAttributeChanged -= TriggerAttributePopUp;
    }
    public void TriggerAttributePopUp(object sender, PlayerStatus.Stats stats, int value)
    {
        StartCoroutine(TriggerAttributePopUp(stats, value));
    }

    public IEnumerator TriggerAttributePopUp(PlayerStatus.Stats stats, int value)
    {
        yield return new WaitUntil(() => attributeAnimator.GetCurrentAnimatorStateInfo(0).IsName("UIAttribPopDownAnimation"));
        image.sprite = attributeImages[(int)stats];
        text.text = value > 0 ? "+" + value : value.ToString();
        text.color = value > 0 ? Color.green : Color.red;
        attributeAnimator.SetTrigger("Trigger");
    }
}
