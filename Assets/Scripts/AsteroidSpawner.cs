using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    float timeSinceSpawn;

    private float spawnDelta = 4f;
    private float spawnHeight = 6f;

    private float randomSpawnDelta;
    private float[] spawnProbabilities = new float[] { 0.50f, 0.30f, 0.12f, 0.08f };
    private float[,] meteorParam = new float[,] {
        { 4f, 1f, 1f },
        { 8f, 2f, 2f },
        { 12f, 4f, 4f },
        { 16f, 6f, 6f }
    };
    private Resource[] resource = new Resource[] {
        new Resource(1,0,1,0),
        new Resource(2,0,0,0),
        new Resource(0,2,0,0),
        new Resource(0,1,0,1)
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
        spawnDelta *= Mathf.Pow(0.8f, Time.deltaTime / 30f);
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn > randomSpawnDelta)
        {
            spawnObject();
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
        return spawnDelta + Random.Range(-0.75f, 0.25f);
    }

    private void spawnObject()
    {
        timeSinceSpawn = 0f;
        randomSpawnDelta = getRandomSpawnTime();

        int meteorI = GetMeteor();
        GameObject newAsteroid = Instantiate(meteor[meteorI], transform);
        AsteroidCycle asteroidCycle = newAsteroid.GetComponent<AsteroidCycle>();
        asteroidCycle.SetParameters(meteorParam[meteorI, 0], meteorParam[meteorI, 1], meteorParam[meteorI, 2], resource[meteorI]);
        float x = Random.Range(GameManager.xMin, GameManager.xMax);
        newAsteroid.transform.position = new Vector3(x, spawnHeight);
        if (Random.Range(0, 1f) > 0.93f)
        {
            newAsteroid.transform.localScale = new Vector3(1, 1, 1);
            Rigidbody2D rb2d = newAsteroid.GetComponent<Rigidbody2D>();
            rb2d.mass = 16f;
            rb2d.drag = 1f;
            Resource scaledResource = resource[meteorI];
            scaledResource.ScaleResource(5);
            asteroidCycle.SetParameters(5 * meteorParam[meteorI, 0], 5 * meteorParam[meteorI, 1], 5 * meteorParam[meteorI, 2], scaledResource);
        }
    }

}
