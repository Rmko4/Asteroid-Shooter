using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCycle : MonoBehaviour
{
    private float health = 10f;
    private float damage = 20f;
    private int score = 1;
    private Resource resources;
    

    private TurretController turretController;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameObject turret = GameObject.Find("Turret");
        turretController = turret.GetComponent<TurretController>();
        GameObject gameManagerObject = GameObject.Find("Game Manager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    public void SetParameters(float health, float damage, float score, Resource resources)
    {
        this.health = health;
        this.damage = damage;
  
        this.score = (int)score;

        this.resources = resources;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < GameManager.yMin || health <= 0f)
        {
            if (health > 0f && transform.position.x > GameManager.xMin - .9f && transform.position.x < GameManager.xMax + .9f)
            {
                turretController.Health -= damage;
            }
            else
            {
                gameManager.Score += score;
                turretController.AddResources(resources);
            }
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
