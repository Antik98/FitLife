using UnityEngine;
using UnityEngine.SceneManagement;
 
public class SceneController : MonoBehaviour {
    public static string prevScene = "";
    public static string currentScene = "";
    public Transform player;

    public virtual void Start() {
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Transform>();
        currentScene = SceneManager.GetActiveScene().name;
    }
 
    public void LoadScene(string sceneName) {
        prevScene = currentScene;
        SceneManager.LoadScene(sceneName);

        if (GameObject.FindGameObjectsWithTag("music").Length != 0)
        {
            ChangeMusicByScene _musicChanger = GameObject.FindGameObjectWithTag("music").GetComponent<ChangeMusicByScene>();
            _musicChanger.updateMusic(sceneName);
        }
    }
     
}