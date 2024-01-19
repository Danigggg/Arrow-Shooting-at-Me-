using UnityEngine;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{

    public void CallGameplayScene()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}
