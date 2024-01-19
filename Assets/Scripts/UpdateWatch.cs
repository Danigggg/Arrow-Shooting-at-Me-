using UnityEngine;
using TMPro;

public class UpdateWatch : MonoBehaviour
{
    public TextMeshProUGUI watchText;
    private float timer = 0.0f;

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }

    void Update()
    {
        timer += Time.deltaTime;
        watchText.text = (timer * 100).ToString("0000");
    }
}
