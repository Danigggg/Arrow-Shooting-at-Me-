using System.Linq;
using UnityEngine;

public class RandomizerLogic : MonoBehaviour
{
    public float max = 1.8f;
    public float min = 0.6f;

    public float minSpeed;
    public float maxSpeed;
    public Spawner[] spawners;

    private float timeOfChange = 1.5f;

    void Start()
    {
        InvokeRepeating("Randomize", timeOfChange, timeOfChange);
    }
 
    private void Randomize()
    {
        for (int i = 0; i < spawners.Length; ++i)
        {
            spawners[i].fireRate = Random.Range(min, max);
            spawners[i].speed = Random.Range(minSpeed, maxSpeed);
        }
    }
}
