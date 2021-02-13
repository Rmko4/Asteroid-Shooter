using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject defaultShot;
    public GameObject gameManagerObject;

    public float movementSpeed = 10.0f;
    public float fireDelta = 0.2f;
    public float bulletSpeed = 10f;

    private float health = 100f;

    private float xOffset = -1.5f;
    private float yOffset = -4.85f;
    private float earthRadius = 100f;
    private GameManager gameManager;

    float timeSinceFire;

    public float Health
    {
        get => health;
        set
        {
            health = value;
            NotifyHealth();
        }
    }

    private void NotifyHealth()
    {
        gameManager.UpdateHealth();
    }

    private void Start()
    {
        timeSinceFire = 0f;
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }


    void Update()
    {

        float translation = Input.GetAxis("Horizontal") * movementSpeed;
        translation *= Time.deltaTime;

        Vector3 position = transform.position;
        position.x += translation;

        if (position.x < GameManager.xMin)
        {
            position.x = GameManager.xMin;
            transform.position = position;
        }
        else if (position.x > GameManager.xMax)
        {
            position.x = GameManager.xMax;
            transform.position = position;
        }
        else
        {
            float newX = position.x - xOffset;
            position.y = Mathf.Sqrt(earthRadius * earthRadius - newX * newX) - earthRadius + yOffset;
            transform.position = position;
        }

        if (Input.GetAxis("Fire1") == 1f && timeSinceFire > fireDelta)
        {
            timeSinceFire = 0;

            Transform barrelTransform = transform.GetChild(0);
            Vector3 positionClone = barrelTransform.position;
            Quaternion rotationClone = barrelTransform.rotation;
            float theta = rotationClone.eulerAngles.z + 90f;
            GameObject cloneShot = Instantiate(defaultShot, positionClone, rotationClone);
            Rigidbody2D rb2d = cloneShot.GetComponent<Rigidbody2D>();

            float x = Mathf.Cos(theta * Mathf.Deg2Rad);
            float y = Mathf.Sin(theta * Mathf.Deg2Rad);

            rb2d.velocity = new Vector2(x, y) * bulletSpeed;
        }
        timeSinceFire += Time.deltaTime;

    }
}
