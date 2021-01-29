using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelControler : MonoBehaviour
{
    public float offset = -90f;
    Camera _camera = null;  // cached because Camera.main is slow, so we only call it once.
    void Start()
    {
        _camera = Camera.main;
    }


    void Update()
    {
        Vector3 difference = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + offset);
    }
}
