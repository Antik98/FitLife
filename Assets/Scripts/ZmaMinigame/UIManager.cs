using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Transform lifeParent = null;

    [SerializeField]
    private GameObject lifePrefab;

    private Stack<GameObject> lives = new Stack<GameObject>(); 

    private static UIManager instance;

    [SerializeField]
    private GameManagerZMA gameManager;

    private void Start()
    {
        GameObject go = GameObject.Find("GameManager");
        gameManager = (GameManagerZMA)go.GetComponent(typeof(GameManagerZMA));
    }
    public static UIManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    public void AddLife(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            lives.Push(Instantiate(lifePrefab, lifeParent));
        }
    }

    public void RemoveLife()
    {
        Destroy(lives.Pop());

        if(lives.Count == 0)
        {
            gameManager.EndGame();
        }
    }
}
