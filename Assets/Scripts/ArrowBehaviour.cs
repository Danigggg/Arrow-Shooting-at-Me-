using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    public Rigidbody2D arrowRB;
    private Vector2 allTimeVelocity;
    private void Start()
    {
        allTimeVelocity = arrowRB.velocity;
    }


    //pause behaviour
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

        if(newGameState == GameState.Paused)
            arrowRB.velocity = Vector2.zero;
        else
            arrowRB.velocity = allTimeVelocity;
    }
}
