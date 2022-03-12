using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassRoomEvent : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject gameController;
    public QuestTracker questManager;
    public GameObject Teacher;
    PopUpMessage popupMessage;

    // Start is called before the first frame update
    void Start()
    {
        // Teacher.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("novotny")[0];
         if(GameObject.FindGameObjectWithTag("StatusController") != null)
             questManager = GameObject.FindGameObjectWithTag("StatusController").GetComponent<QuestTracker>();
         //if (questManager.CheckSchoolQuest().Item1 > 0)
         //{
         //    switch (questManager.CheckSchoolQuest().Item1)
         //    {
         //        case 0:
         //            if (Input.GetKeyDown("e"))
         //            {
         //                // Teacher.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("novotny")[0];
         //                Debug.Log("Completed 0");
         //            }
         //            break;
                
         //        case 4:
         //            if (Input.GetKeyDown("e"))
         //            {
         //                //Teacher.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("kalvoda")[0];
         //                Debug.Log("Completed 4");
        
         //            }
         //            break;
                 
         //        case 8:
         //            if (Input.GetKeyDown("e"))
         //            {
         //                //Teacher.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("vagner")[0];
         //                Debug.Log("Completed 8");
        
         //            }
         //            break;
                 
         //        case 12:
         //            if (Input.GetKeyDown("e"))
         //            {
         //                //Teacher.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("stary")[0];
         //                Debug.Log("Completed 12");
        
         //            }
         //            break;
         //    }
         //}
         popupMessage = gameController.GetComponent<PopUpMessage>();
         StartCoroutine(DisplayText());
    }

    IEnumerator DisplayText()
    {
        GameTimer _gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        _gameTimer.StopTimer();
        yield return new WaitForSeconds(2);
        popupMessage.Open(dialogue, Teacher.GetComponent<SpriteRenderer>().sprite);
        yield return new WaitUntil(() => !popupMessage.isActive());
        GetComponent<ChangeScene>().Activate();
    }


}
