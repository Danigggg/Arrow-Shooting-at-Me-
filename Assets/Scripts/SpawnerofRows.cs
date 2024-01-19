using System;
using System.Collections;
using UnityEngine;

public class SpawnerofRows : MonoBehaviour
{
    public Rigidbody2D  arrowPrefab;
    public Spawner      spawnerSpeed;
    public int          numberOfArrows; //must be odd (design, personal choice)
    public float        spacing = 1.0f;

    private readonly float      repeatingInstance = 6.5f;
    private float               timePauseCalled;
    private float               timeNextInvoke;

    //offset for positioning
    private readonly float radiusOffset = 10.0f + 1.0f;
    private enum Sides { Up, Down, Left, Right }

    private void Start()
    {
        timeNextInvoke = Time.unscaledTime + repeatingInstance;
        InvokeRepeating("ChooseSide", repeatingInstance, repeatingInstance);
    }


    //pause behaviour------------------------------------------------------------------------------------
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
        {
            CancelInvoke("ChooseSide");
            timePauseCalled = Time.unscaledTime;
        }

        if (newGameState == GameState.Gameplay)
        {
            InvokeRepeating("ChooseSide", timeNextInvoke - timePauseCalled, repeatingInstance);
        }
    }
    //pause behaviour------------------------------------------------------------------------------------

    private void ChooseSide()
    {
        Tree(UnityEngine.Random.Range(0, 4));
    }

    private void Tree(int side)
    {
        switch ((Sides)side) 
        { 
        case Sides.Up:
                transform.position = new Vector2(transform.position.x, radiusOffset);
                Spawn(new Vector3(0.0f, 0.0f, -90.0f));
                break;

        case Sides.Down:
                transform.position = new Vector2(transform.position.x, -1 * radiusOffset);
                Spawn(new Vector3(0.0f, 0.0f, 90.0f));
                break;

        case Sides.Left:
                transform.position = new Vector2(-1 * radiusOffset, transform.position.y);
                Spawn(new Vector3(0.0f, 0.0f, 0.0f));
                break;

        case Sides.Right:
                transform.position = new Vector2(radiusOffset, transform.position.y);
                Spawn(new Vector3(0.0f, 0.0f, -180.0f));
                break;
        }
    }

    private void Spawn(Vector3 rotateVector)
    {
        Vector2 speed = new Vector2(spawnerSpeed.speed * Mathf.Cos(rotateVector.z * Mathf.PI/180.0f), spawnerSpeed.speed * Mathf.Sin(rotateVector.z * Mathf.PI / 180.0f));

        float divide = Mathf.Abs(rotateVector.z) == 90.0f ? transform.position.x : transform.position.y;
        float startingPoint = divide - (spacing * numberOfArrows) / 2; //from center to half

        for (int i = 0; i < numberOfArrows; i++)
        {
            Rigidbody2D arrow = Instantiate(arrowPrefab, Mathf.Abs(rotateVector.z) == 90.0f ? new Vector2(startingPoint, transform.position.y) : new Vector2(transform.position.x, startingPoint), Quaternion.Euler(rotateVector));
            arrow.velocity = speed;

            startingPoint += spacing;
        }

        timeNextInvoke = Time.unscaledTime + repeatingInstance;
        transform.position = new Vector2();
    }
}
