using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    float timeSinceSpawn;

    public float spawnRate = 5f;
    public float spawnHeight = 6f;
    public GameObject asteroid;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn > spawnRate)
        {
            timeSinceSpawn = 0f;

            GameObject newAsteroid = Instantiate(asteroid, transform);
            float x = Random.Range(GameManager.xMin, GameManager.xMax);
            newAsteroid.transform.position = new Vector3(x, spawnHeight);
        }
    }
}
