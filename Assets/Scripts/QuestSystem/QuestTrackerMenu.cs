using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrackerMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectQuest()
    {
        GetComponent<Text>().color = Color.red;
    }

    public void DeselectQuest()
    {
        GetComponent<Text>().color = Color.black;
    }
}
