using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCycle : MonoBehaviour
{
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        health = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < GameManager.yMin || health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
