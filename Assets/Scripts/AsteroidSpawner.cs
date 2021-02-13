using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    float timeSinceSpawn;

    public float spawnDelta = 3f;
    public float spawnHeight = 6f;

    private float randomSpawnDelta;
    private float[] spawnProbabilities = new float[] { 0.86f, 0.08f, 0.05f, 0.01f };
    private float[,] meteorParam = new float[,] {
        { 4f, 1f, 1f },
        { 8f, 2f, 2f },
        { 12f, 4f, 4f },
        { 16f, 6f, 6f }
    };

    public GameObject[] meteor = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        timeSinceSpawn = 0f;
        randomSpawnDelta = getRandomSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {
        spawnDelta -= Time.deltaTime / 60f;
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn > randomSpawnDelta)
        {
            timeSinceSpawn = 0f;
            randomSpawnDelta = getRandomSpawnTime();

            int meteorI = GetMeteor();
            GameObject newAsteroid = Instantiate(meteor[meteorI], transform);
            AsteroidCycle asteroidCycle = newAsteroid.GetComponent<AsteroidCycle>();
            asteroidCycle.SetParameters(meteorParam[meteorI, 0], meteorParam[meteorI, 1], meteorParam[meteorI, 2]);
            float x = Random.Range(GameManager.xMin, GameManager.xMax);
            newAsteroid.transform.position = new Vector3(x, spawnHeight);
        }
    }

    private int GetMeteor()
    {
        float prob = Random.Range(0f, 1f);
        float total = 0f;
        for (int i = 0; i < 4; i++)
        {
            total += spawnProbabilities[i];
            if (prob <= total)
            {
                return i;
            }

        }
        return 0;
    }

    private float getRandomSpawnTime()
    {
        return spawnDelta + Random.Range(0f, 0.5f);
    }
}
