using System.Collections;
using System.Collections.Generic;
using GradingSystem;
using UnityEngine;
using UnityEngine.UI;

public class ItemListDisplay : MonoBehaviour
{

    public List<GameObject> itemObjects;
    public GameObject content;
    public GameObject template;

    public void AddItem(string itemText)
    {
            var copy = Instantiate(template);
            copy.GetComponent<Text>().text = itemText;  
            copy.transform.SetParent( content.GetComponent<GridLayoutGroup>().transform, false);
            itemObjects.Add(copy.gameObject);
    }

}


