using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCycle : MonoBehaviour
{
    public float health = 10f;
    public float damage = 20f;
    public int score = 1;

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

    public void SetParameters(float health, float damage, float score)
    {
        this.health = health;
        this.damage = damage;
  
        this.score = (int)score;
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
            }
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
