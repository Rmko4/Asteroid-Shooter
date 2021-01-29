using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    float timeSinceSpawn;

    public float spawnDelta = 3f;
    public float spawnHeight = 6f;

    private float randomSpawnDelta;
    public GameObject asteroid;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceSpawn = 0f;
        randomSpawnDelta = getRandomSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn > randomSpawnDelta)
        {
            timeSinceSpawn = 0f;
            randomSpawnDelta = getRandomSpawnTime();

            GameObject newAsteroid = Instantiate(asteroid, transform);
            float x = Random.Range(GameManager.xMin, GameManager.xMax);
            newAsteroid.transform.position = new Vector3(x, spawnHeight);
        }
    }

    private float getRandomSpawnTime()
    {
        return spawnDelta + Random.Range(-1.5f, 1.5f);
    }
}
