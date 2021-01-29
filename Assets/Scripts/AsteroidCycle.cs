using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCycle : MonoBehaviour
{
    public float health;
    public float damage;

    private TurretController turretController;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameObject turret = GameObject.Find("Turret");
        turretController = turret.GetComponent<TurretController>();
        GameObject gameManagerObject = GameObject.Find("Game Manager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        health = 10f;
        damage = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < GameManager.yMin || health <= 0f)
        {
            if (health > 0f && transform.position.x > GameManager.xMin - .5f && transform.position.x < GameManager.xMax + .5f)
            {
                turretController.Health -= damage;
            }
            else
            {
                gameManager.Score += 1;
            }
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
