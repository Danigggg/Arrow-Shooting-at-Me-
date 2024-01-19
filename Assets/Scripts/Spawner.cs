using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Rigidbody2D  arrowPreFab;
    public Transform    player;
    public float        speed = 10.0f;

    public float        fireRate = 1.2f;
    private float       timer = 0.0f;

    void Update()
    {
        if (timer < fireRate)
        {
            timer += Time.deltaTime;
            return;
        }

        Shoot();
    }

    private void Shoot()
    {
        float aimPlayer = RotateToTarget();
        //angleToRotateTo is in radians and cos sin use radians no need to use degrees
        Vector2 velocity = new Vector2(speed * Mathf.Cos(aimPlayer), speed * Mathf.Sin(aimPlayer));
        
        //euler uses degrees
        Rigidbody2D arrow = Instantiate(arrowPreFab, transform.position, Quaternion.Euler(0.0f, 0.0f, aimPlayer * 180.0f / Mathf.PI));
        arrow.velocity = velocity;

        timer = 0.0f;
    }

    private float RotateToTarget()
    {
        float x = player.position.x - transform.position.x;
        float y = player.position.y - transform.position.y;
        return Mathf.Atan2(y,x);
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
    }
}
