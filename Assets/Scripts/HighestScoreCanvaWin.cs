using UnityEngine;
using TMPro;

public class HighestScoreCanvaWin : MonoBehaviour
{
    public LogicManager     logicManager;
    public TextMeshProUGUI  text;
    void Start()
    {

        text.text = "New Highscore: " + PlayerPrefs.GetInt(logicManager.highScorePlayerPrefsKey);

    }
}
