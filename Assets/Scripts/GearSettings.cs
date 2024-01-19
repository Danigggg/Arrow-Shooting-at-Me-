using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GearSettings : MonoBehaviour
{
    public Canvas           settingsCanva;
    public SpriteRenderer   playerColor;
    public Slider           slider;
    public PauseController  pauseController;
    public SpriteRenderer   playerCopy;  

    public AudioSource      gameTheme;

    string colorKey = "PlayerColor";
    string volumeSliderKey = "SliderVolume";

    private bool gearsOpen = false;

    private void Start()
    {
        LoadColor();
        LoadVolume();
        LoadPlayerCopy();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //this will call the pause and the settings opening
        {
            ClickedSettings(true);
        } 
    }

    public void ClickedSettings(bool calledFromKey) //using this function that can be called via keycode escape or clicking on gears
    {
        if (!calledFromKey) //if the functions is called from escape, PauseController pauses the game when escape is pressed, u don't need it, if it is called via button call pause
        {
            if(pauseController.enabled)
                pauseController.PauseOrUnpause();
        }

        gearsOpen = !gearsOpen; //this whill change from true to false and from false to true

        if (gearsOpen)
        {
            OpenSettings();
        }

        if (!gearsOpen)
        {
            CloseSettings();
        }
    }


    private void OpenSettings()
    {
        gameTheme.Pause();
        settingsCanva.gameObject.SetActive(true);
    }


    private void CloseSettings()
    {
        if(pauseController.enabled)
            gameTheme.Play();
        settingsCanva.gameObject.SetActive(false);
    }


    public void ChangeColor(Button button)
    {
        playerColor.color = button.image.color;
        string colorSaved = ColorToHex(button.image.color);
        PlayerPrefs.SetString(colorKey,colorSaved);
        LoadPlayerCopy();
    }


    private void LoadColor()
    {
        if (PlayerPrefs.HasKey(colorKey))
        {
            string colorString = PlayerPrefs.GetString(colorKey);
            playerColor.color = HexToColor(colorString);
        }
    }

    private string ColorToHex(Color color)
    {
        int r = Mathf.RoundToInt(color.r * 255f);
        int g = Mathf.RoundToInt(color.g * 255f);
        int b = Mathf.RoundToInt(color.b * 255f);
        int a = Mathf.RoundToInt(color.a * 255f);

        return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);
    }

    private Color HexToColor(string hex)
    {
        hex = hex.Replace("#", "");

        if (hex.Length >= 6)
        {
            int r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            // Check for alpha channel
            if (hex.Length >= 8)
            {
                int a = int.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
                return new Color32((byte)r, (byte)g, (byte)b, (byte)a);
            }
            else
            {
                return new Color32((byte)r, (byte)g, (byte)b, 255);
            }
        }
        return Color.white;
    }

    private void LoadPlayerCopy()
    {
        playerCopy.color = playerColor.color;
    }

    private void LoadVolume()
    {
        if (!PlayerPrefs.HasKey(volumeSliderKey))
        {
            Debug.Log("Hi");
            PlayerPrefs.SetFloat(volumeSliderKey, 0.0f);
        }

        slider.value = PlayerPrefs.GetFloat(volumeSliderKey);
        gameTheme.volume = PlayerPrefs.GetFloat(volumeSliderKey);
    }

    public void ModifyVolumeWithSlider()
    {
        gameTheme.volume = slider.value;
        PlayerPrefs.SetFloat(volumeSliderKey, slider.value);
    }

}
