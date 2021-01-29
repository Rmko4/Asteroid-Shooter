using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public float speed = 10.0f;

    void Update()
    {

        float translation = Input.GetAxis("Horizontal") * speed;
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
            transform.Translate(translation, 0, 0);
        }



    }
}
