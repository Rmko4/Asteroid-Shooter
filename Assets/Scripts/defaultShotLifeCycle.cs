using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defaultShotLifeCycle : MonoBehaviour
{
    public float lifeTime = 2f;
    public float damage = 2f;

    float aliveTime;
    float timeAfterTrigger;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        aliveTime = 0;
        timeAfterTrigger = .1f;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAliveTime();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        AsteroidCycle asteroid = collision.gameObject.GetComponent<AsteroidCycle>();
        if (asteroid != null)
        {
            asteroid.TakeDamage(damage);
        }

        sprite.enabled = false;
        aliveTime = lifeTime - timeAfterTrigger;
    }

    private void CheckAliveTime()
    {
        aliveTime += Time.deltaTime;
        if (aliveTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
