using TMPro;
using UnityEngine;

public class HighestScoreScript : MonoBehaviour
{
    public TextMeshProUGUI highestScoreText;
    void Start()
    {
        highestScoreText.text = "Highest Score: " + PlayerPrefs.GetInt("Highscore");
    }

}
