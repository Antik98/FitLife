using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : DisplayHint {
    [SerializeField] private string toScene = "";
    private SceneController sceneController;
    private GameObject player;
    private float moveSpeed;
    private string displayTextOnHint = "Cestuj zmáčknutím E";
    public bool playSound = true;
    public bool CollisionActivated = true;
    private bool forceInvoked = false;

    // animation
    private Animator transition;
    public float transitionTime = 1.5f;

    // sound
    private AudioSource audioPlayer;
    public AudioClip transitionSFX;
    private float audioVolume = 0.5f;

    bool AnimatorIsPlaying()
    {
        return transitionTime + 0.2f >
               transition.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    void Start() {
        sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneController>();

        var fade = GameObject.Find("Fade");
        if (fade ?? false)
            transition = fade.GetComponent<Animator>();

        audioPlayer = gameObject.GetComponent<AudioSource>();

        if (audioPlayer ?? false)
            audioPlayer.volume = audioVolume;
        GameTimer _gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        _gameTimer.StartTimer();
        player = GameObject.FindGameObjectWithTag("Player");

        labelText = displayTextOnHint;

    }

    private void OnEnable()
    {
        StartCoroutine(WaitForTransition());
    }

    IEnumerator WaitForTransition()
    {
        yield return new WaitForSecondsRealtime(transitionTime);
        StatusController.Instance.GetComponent<CoroutineQueue>().TriggerSceneChanged(SceneManager.GetActiveScene().name);
    }

    public override void Action()
    {
        if (CollisionActivated)
        {
            if (HasCollided() && Input.GetKeyDown("e"))
            {
                Close();

                if (playSound)
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

                if (playSound)
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
        sceneController.LoadScene(toScene);
    }

    public void Activate()
    {
        forceInvoked = true;
        GameTimer _gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        _gameTimer.StopTimer();
        StartCoroutine(LoadNextScene(toScene));
    } 
    public void Activate(string customScene)
    {
        if (!string.IsNullOrEmpty(customScene))
        {
            forceInvoked = true;
            this.toScene = customScene; 
            StartCoroutine(LoadNextScene(customScene));
        }
    }
}