using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class SlowMo : MonoBehaviour
{
    public float            slowSpeedTime = 0.4f;
    public TextMeshProUGUI  slowMoText;

    public float            slowmoDurationTimerHandler = 5.0f;
    private float           slowmoDurationTimer;

    public float            slowMoCooldownHandler = 7.0f;
    private float           slowMoCooldown;
    
    private enum TextState { DisplayingAvaiable, DisplayingSlowDuration, DisplayingCooldown, Paused}
    private TextState currentTextState = TextState.DisplayingAvaiable;

    //to avoid 12 years old idiot spammers kids fuckers
    private bool coroutineRunning = false;


    private void Start()
    {
        Time.timeScale = 1.0f;
        slowmoDurationTimer = slowmoDurationTimerHandler;
        slowMoCooldown = slowMoCooldownHandler;
    }


    //pause behaviour------------------------------------------------------------------------------------------------------
    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }


    private TextState OldStateHandler; // this gets the last state before the game gets paused
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;

        if (newGameState == GameState.Paused)
        {
            OldStateHandler = currentTextState;
            OnPause();
        }

        if (newGameState == GameState.Gameplay)
        {
            FromPauseToGameplay(OldStateHandler);
        }
    }

    private void OnPause()
    {
        currentTextState = TextState.Paused;
    }


    private void FromPauseToGameplay(TextState OldState)
    {
        currentTextState = OldState;
    }

    //end of pause behaviour------------------------------------------------------------------------------------------------------


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !coroutineRunning) //coroutineRunning handles slowmoduration and cooldown
        {
            Slow();
            StartCoroutine(StartTimer());
        }

        switch (currentTextState)
        {
            case TextState.DisplayingAvaiable:
                slowMoText.text = "SlowMo Avaiable";
                break;

            case TextState.DisplayingSlowDuration:
                slowMoText.text = "SlowMo Duration: " + slowmoDurationTimer.ToString("f2"); ;
                break;

            case TextState.DisplayingCooldown:
                slowMoText.text = "Cooldown: " + slowMoCooldown.ToString("f2");
                break;

            case TextState.Paused:
                //this will lock the text
                break;
        }
    }

    private void Slow()
    {
        Time.timeScale = slowSpeedTime;
    }

    private IEnumerator StartTimer()
    {
        coroutineRunning = true;
        currentTextState = TextState.DisplayingSlowDuration;

        while(slowmoDurationTimer > 0.0f)
        {
            if(currentTextState != TextState.Paused)
                slowmoDurationTimer -= Time.unscaledDeltaTime;
            yield return null;
        }

        SlowMoEnds();
    }

    //all actions after slowmo effect ends
    private void SlowMoEnds()
    {
        StartCoroutine(StartCooldown());
        Time.timeScale = 1.0f;
        slowmoDurationTimer = slowmoDurationTimerHandler;
    }

    private IEnumerator StartCooldown()
    {
        currentTextState = TextState.DisplayingCooldown;

        while (slowMoCooldown > 0.0f)
        {
            if (currentTextState != TextState.Paused)
                slowMoCooldown -= Time.unscaledDeltaTime;
            yield return null;
        }

        coroutineRunning = false;

        slowMoCooldown = slowMoCooldownHandler;

        //if cooldowns ends, it's avaiable again
        currentTextState = TextState.DisplayingAvaiable;
    }

}
