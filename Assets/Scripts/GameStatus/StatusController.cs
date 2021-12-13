using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{

    private static StatusController _instance;

    public static StatusController Instance { get { return _instance; } }

    // Start is called before the first frame update
    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" || _instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public void Reset()
    {
        GetComponent<QuestTracker>()?.Reset();
        GetComponent<GameTimer>()?.Reset();
        GetComponent<PlayerStatus>()?.Reset();
    }
}
