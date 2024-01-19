using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    public TextMeshProUGUI runningScore;
    public string highScorePlayerPrefsKey = "Highscore";
    public AudioSource gameTheme;

    [Header("Canvas")]
    [SerializeField] Canvas winCanva;
    [SerializeField] Canvas loseCanva;

    [Header("Scripts To Disable")]
    public UpdateWatch      runningScoreScript;
    public PauseController  pauseControllerScript;
    public SlowMo           slowMoScript;

    public void OnPlayerDeath()
    {
        gameTheme.Stop();

        DisableScripts();
        int endingResult = int.Parse(runningScore.text);
        
        if (PlayerPrefs.GetInt(highScorePlayerPrefsKey) > endingResult)
        {
            ShowLosingCanva();
        }
        else
        {
            PlayerPrefs.SetInt(highScorePlayerPrefsKey, endingResult);
            ShowWinningCanva();
        }
    }

    private void DisableScripts()
    {
        runningScoreScript.enabled = false;
        pauseControllerScript.enabled = false;
        slowMoScript.enabled = false;
    }

    private void ShowWinningCanva()
    {
        winCanva.gameObject.SetActive(true);
    }

    private void ShowLosingCanva()
    {
        loseCanva.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void Quit()
    {
        Application.Quit();
    }

}
