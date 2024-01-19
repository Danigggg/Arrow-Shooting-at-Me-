using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuGears : MonoBehaviour
{
    public Canvas settingsCanva;
    public AudioSource gameTheme;
    public UnityEngine.UI.Slider volumeSlider;
    private string volumeSliderKey = "SliderVolume";
    private bool gearsOpen = false;

    private void Start()
    {
        gameTheme.Play();
        volumeSlider.value = PlayerPrefs.GetFloat(volumeSliderKey, 0.0f);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //this will call the pause and the settings opening
        {
            ClickedSettings(true);
        }
    }

    public void ClickedSettings(bool calledFromKey) //using this function that can be called via keycode escape or clicking on gears
    {

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
        gameTheme.Play();
        settingsCanva.gameObject.SetActive(false);
    }



    public void ChangeVolume()
    {
        gameTheme.volume = volumeSlider.value;
        PlayerPrefs.SetFloat(volumeSliderKey, volumeSlider.value);
    }

}