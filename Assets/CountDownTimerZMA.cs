using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimerZMA : MonoBehaviour
{

    public float timer = 30;
    private bool canCount = true;
    public Text timerText;

    // Start is called before the first frame update
    private void Start()
    {
    }
    // Update is called once per frame 
    void Update()
    {
        if (timer >= 0f && canCount)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString();
        }
    }
}
