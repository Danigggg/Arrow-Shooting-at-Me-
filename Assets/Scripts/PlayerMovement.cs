using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float            speed = 20.0f;
    public Transform        arena;
    public CircleCollider2D circleColliderPlayer;
    public UnityEvent       OnPlayerDeathEvent;


    private Vector2         originPoint;
    private float           radius = 10.0f;
    private float           arenaOffset;
    private bool            isAlive = true;

    private void Start()
    {
        arenaOffset = radius - circleColliderPlayer.radius;
    }


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
        if (Vector2.Distance(transform.position, originPoint) > arenaOffset)
        {
            transform.localPosition = transform.localPosition.normalized * arenaOffset;
        }

        if (isAlive)
        {
            Vector2 inputKeyboard = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            transform.position = (Vector2)transform.position + inputKeyboard * Time.unscaledDeltaTime * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        {
            PlayerKilled();
            OnPlayerDeathEvent.Invoke();
        }
    }

    private void PlayerKilled()
    {
        isAlive = false;
        circleColliderPlayer.isTrigger = false;
    }
}
