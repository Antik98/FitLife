using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenOnEnter : MonoBehaviour
{
    private bool loaded = false; 

    private ChangeScene sceneChanger; 
    // Start is called before the first frame update
    void Start()
    {
        sceneChanger = GetComponent<ChangeScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) && !loaded && (sceneChanger ?? false))
        {
            loaded = true; 
            Debug.Log("Trying to load screen");
            sceneChanger.Activate();
        }
    }
}
