using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : DisplayHint {
    [SerializeField] private string toScene = "";
    private SceneController sceneController;
    private GameObject player;
    private float moveSpeed;
    public string displayTextOnHint = "Cestuj zmáčknutím E";
    public bool CollisionActivated = true;
    private bool forceInvoked = false;

    // animation
    private Animator transition;
    public float transitionTime = 1;

    // sound
    private AudioSource audioPlayer;
    public AudioClip transitionSFX;
    private float audioVolume = 0.5f;

    void Start() {
        sceneController = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<SceneController>();
        if (!CollisionActivated)
            displayHint = false;
        var fade = GameObject.Find("Fade");
        if (fade ?? false)
            transition = fade.GetComponent<Animator>();

        audioPlayer = gameObject.GetComponent<AudioSource>();

        if (audioPlayer ?? false)
            audioPlayer.volume = audioVolume;
        GameObject.FindGameObjectWithTag("StatusController")?.GetComponent<GameTimer>()?.StartTimer(); ;
        player = GameObject.FindGameObjectWithTag("Player");

        labelText = displayTextOnHint;

    }

    private void OnEnable()
    {
        StartCoroutine(WaitForTransition());
    }

    IEnumerator WaitForTransition()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitWhile(() => transition?.IsInTransition(0) ?? false);
        StatusController.Instance?.coroutineQueue?.TriggerSceneChanged(SceneManager.GetActiveScene().name);
    }

    public override void Action()
    {
        if (CollisionActivated)
        {
            if (HasCollided() && Input.GetKeyDown("e"))
            {
                Close();

                if (transitionSFX != null && audioPlayer != null)
                {
                    audioPlayer.clip = transitionSFX;
                    audioPlayer.PlayOneShot(transitionSFX);
                }
                lockPlayer();
                Activate();
            }
        }
        else
        {
            if (forceInvoked)
            {
                forceInvoked = false;
                Close();

                if (transitionSFX != null && audioPlayer != null)
                {
                    audioPlayer.clip = transitionSFX;
                    audioPlayer.PlayOneShot(transitionSFX);
                }
                lockPlayer();
                Activate();
            }
        }
    }


    private void lockPlayer() {
        if(player ?? false)
        {
            player.GetComponent<playerMovement>().lockPlayer();
        }
    }

    IEnumerator LoadNextScene (string toScene) {
        if(transition??false)
		    transition.SetTrigger("Start");

		yield return new WaitForSecondsRealtime(transitionTime);
        sceneController?.LoadScene(toScene);
    }

    public void Activate()
    {
        GameTimer _gameTimer = GameObject.FindGameObjectWithTag("StatusController")?.GetComponent<GameTimer>();
        _gameTimer?.StopTimer();
        StartCoroutine(LoadNextScene(toScene));
    } 
    public void Activate(string customScene)
    {
        if (!string.IsNullOrEmpty(customScene))
        {
            GameTimer _gameTimer = GameObject.FindGameObjectWithTag("StatusController")?.GetComponent<GameTimer>();
            _gameTimer?.StopTimer();
            this.toScene = customScene; 
            StartCoroutine(LoadNextScene(customScene));
        }
    }
}