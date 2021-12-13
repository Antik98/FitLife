using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.FindWithTag("music"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
